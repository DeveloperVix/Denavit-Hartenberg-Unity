using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoboticArm : MonoBehaviour
{
    private static RoboticArm instance;
    public static RoboticArm Instance { get => instance; }


    [Header("Actual link")]
    public Transform curRedCylinder;
    public SetPositionRotation pointRedAxis;
    public Transform curBlueCylinder;
    public SetPositionRotation pointBlueAxis;


    [Header("Options Nodes")]
    public TMP_Dropdown optionsLink;
    Slider curSlider;

    [Header("Type rotation")]
    public Toggle rotationalType;
    public Toggle prismaticType;

    [Header("Teta Slider")]
    public Slider tetaSlider;
    public InputField inputTeta;
    float lastTeta;

    [Header("D Slider")]
    public Slider dSlider;
    float previousValueD;
    public InputField inputD;

    [Header("A Slider")]
    public Slider aSlider;
    float previousValueA;
    public InputField inputA;

    [Header("Alfa Slider")]
    public Slider alfaSlider;
    public InputField inputAlfa;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        optionsLink.ClearOptions();
        tetaSlider.onValueChanged.AddListener(ControlTeta);
        tetaSlider.onValueChanged.AddListener(InputTeta);
        dSlider.onValueChanged.AddListener(ControlD);
        dSlider.onValueChanged.AddListener(InputD);
        aSlider.onValueChanged.AddListener(ControlA);
        alfaSlider.onValueChanged.AddListener(ControlAlfa);

        inputTeta.text = "0";
        inputD.text = "0";
        inputA.text = "0";
        inputAlfa.text = "0";
    }

    //Based on the option selected
    public void SetCurrentLink()
    {
        curRedCylinder = DH_Calculates.Instance.allLinks[optionsLink.value].redCylinder.transform;
        pointRedAxis = DH_Calculates.Instance.allLinks[optionsLink.value].pointAxisRedCylinder.GetComponent<SetPositionRotation>();

        curBlueCylinder = DH_Calculates.Instance.allLinks[optionsLink.value].blueCylinder.transform;
        pointBlueAxis = DH_Calculates.Instance.allLinks[optionsLink.value].pointAxisBlueCylinder.GetComponent<SetPositionRotation>();

        tetaSlider.onValueChanged.RemoveListener(ControlTeta);
        dSlider.onValueChanged.RemoveListener(ControlD);
        aSlider.onValueChanged.RemoveListener(ControlA);
        alfaSlider.onValueChanged.RemoveListener(ControlAlfa);

        curSlider = DH_Calculates.Instance.allLinks[optionsLink.value].teta_d_Slider.GetComponent<Slider>();

        if (rotationalType.isOn)
        {
            
            curSlider.onValueChanged.AddListener(ControlTeta);
            curSlider.onValueChanged.AddListener(InputTeta);
            curSlider.maxValue = 360;
            curSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "θ"+optionsLink.value;
            curSlider.value = DH_Calculates.Instance.allLinks[optionsLink.value].teta;
        }
        else
        {
            curSlider.onValueChanged.AddListener(ControlD);
            curSlider.onValueChanged.AddListener(InputD);
            curSlider.maxValue = 10;
            curSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "d"+optionsLink.value;
            curSlider.value = DH_Calculates.Instance.allLinks[optionsLink.value].d;
        }

        inputTeta.text = DH_Calculates.Instance.allLinks[optionsLink.value].teta.ToString();
        inputD.text = DH_Calculates.Instance.allLinks[optionsLink.value].d.ToString();
        inputA.text = DH_Calculates.Instance.allLinks[optionsLink.value].a.ToString();
        inputAlfa.text = DH_Calculates.Instance.allLinks[optionsLink.value].alfa.ToString();

        previousValueA = DH_Calculates.Instance.allLinks[optionsLink.value].a;
        previousValueD = DH_Calculates.Instance.allLinks[optionsLink.value].d;

        tetaSlider.onValueChanged.AddListener(ControlTeta);
        dSlider.onValueChanged.AddListener(ControlD);
        aSlider.onValueChanged.AddListener(ControlA);
        alfaSlider.onValueChanged.AddListener(ControlAlfa);
    }

    public void RotationalType()
    {
        if (rotationalType.isOn)
        {
            prismaticType.isOn = false;
            curSlider.onValueChanged.RemoveAllListeners();
            //Show teta
            curSlider.onValueChanged.AddListener(ControlTeta);
            curSlider.onValueChanged.AddListener(InputTeta);
            curSlider.maxValue = 360;

            curSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "θ"+optionsLink.value;
            curSlider.value = DH_Calculates.Instance.allLinks[optionsLink.value].teta;
        }
    }

    public void PrismaticType()
    {
        if (prismaticType.isOn)
        {
            rotationalType.isOn = false;
            curSlider.onValueChanged.RemoveAllListeners();
            //Show d
            curSlider.onValueChanged.AddListener(ControlD);
            curSlider.onValueChanged.AddListener(InputD);
            curSlider.maxValue = 10;

            curSlider.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "d"+optionsLink.value;
            curSlider.value = DH_Calculates.Instance.allLinks[optionsLink.value].d;
        }
    }

    #region Teta
    public void ControlTeta(float value)
    {
       // Debug.Log("Modifico Rotación en radio");
        curBlueCylinder.rotation = Quaternion.Slerp(curRedCylinder.localRotation, Quaternion.Euler(0f, value, 0), 1f);

        DH_Calculates.Instance.allLinks[optionsLink.value].teta = value;
        curSlider.value = value;

        BlueRotate();
    }

    //Called from the Teta input field event, when the user enter data
    public void SetInputTeta()
    {
        tetaSlider.value = float.Parse(inputTeta.text);
    }

    //Called from the Teta slider event
    public void InputTeta(float value)
    {
        inputTeta.text = value.ToString();

    }
    #endregion

    #region D
    public void ControlD(float value)
    {
        float d = value - previousValueD;
        curBlueCylinder.localScale += Vector3.up * d;
        previousValueD = value;

        DH_Calculates.Instance.allLinks[optionsLink.value].d = value;
        curSlider.value = value;

        BlueFinishRotation();
    }

    public void SetInputD()
    {
        dSlider.value = float.Parse(inputD.text);
    }

    public void InputD(float value)
    {
        inputD.text = value.ToString();
    }

    #endregion

    #region A
    public void ControlA(float value)
    {
        float a = value - previousValueA;
        curRedCylinder.localScale += Vector3.up * a;
        previousValueA = value;

        DH_Calculates.Instance.allLinks[optionsLink.value].a = value;
    }

    //Called from the A input field event, when the user enter data
    public void SetInputA()
    {
        aSlider.value = float.Parse(inputA.text);
    }

    //Called from the A slider event
    public void InputA()
    {
        inputA.text = aSlider.value.ToString();
    }
    #endregion

    #region Alfa
    public void ControlAlfa(float value)
    {
        Debug.Log("Modifico Rotación acostado");
        curRedCylinder.localRotation = Quaternion.Slerp(curRedCylinder.localRotation, Quaternion.Euler(value, DH_Calculates.Instance.allLinks[optionsLink.value].teta, 270), 1f);

        DH_Calculates.Instance.allLinks[optionsLink.value].alfa = value;
    }

    //Called from the alfa input field event, when the user enter data
    public void SetInputAlfa()
    {
        alfaSlider.value = float.Parse(inputAlfa.text);
    }

    //Called from the alfa slider event
    public void InputAlfa()
    {
        inputAlfa.text = alfaSlider.value.ToString();
    }
    #endregion

    public void BlueRotate()
    {
        for (int i = optionsLink.value; i < DH_Calculates.Instance.allLinks.Count; i++)
        {
            DH_Calculates.Instance.allLinks[i].pointAxisRedCylinder.GetComponent<SetPositionRotation>().Rotating();
            DH_Calculates.Instance.allLinks[i].redCylinder.transform.SetParent(curBlueCylinder);
            DH_Calculates.Instance.allLinks[i].redCylinder.transform.localScale = new Vector3(1, DH_Calculates.Instance.allLinks[i].redCylinder.transform.localScale.y, 1);

            DH_Calculates.Instance.allLinks[i].pointAxisBlueCylinder.GetComponent<SetPositionRotation>().Rotating();
            DH_Calculates.Instance.allLinks[i].blueCylinder.transform.SetParent(curBlueCylinder);
        }
    }

    public void BlueFinishRotation()
    {
        for (int i = optionsLink.value; i < DH_Calculates.Instance.allLinks.Count; i++)
        {
            DH_Calculates.Instance.allLinks[i].redCylinder.transform.SetParent(null);
            DH_Calculates.Instance.allLinks[i].redCylinder.transform.localScale = new Vector3(1, DH_Calculates.Instance.allLinks[i].redCylinder.transform.localScale.y, 1);
            DH_Calculates.Instance.allLinks[i].pointAxisRedCylinder.GetComponent<SetPositionRotation>().FinishRotation();

            DH_Calculates.Instance.allLinks[i].blueCylinder.transform.SetParent(null);
            DH_Calculates.Instance.allLinks[i].pointAxisBlueCylinder.GetComponent<SetPositionRotation>().FinishRotation();
        }
    }

    public void RotateRedCylinderAxis()
    {
        pointRedAxis.Rotating();
        RedRotate();
    }

    public void FinishRotationRedCylinderAxis()
    {
        pointRedAxis.FinishRotation();
        BlueFinishRotation();
    }

    public void RedRotate()
    {
        for (int i = optionsLink.value + 1; i < DH_Calculates.Instance.allLinks.Count; i++)
        {
            DH_Calculates.Instance.allLinks[i].pointAxisRedCylinder.GetComponent<SetPositionRotation>().Rotating();
            DH_Calculates.Instance.allLinks[i].redCylinder.transform.SetParent(curRedCylinder);

            DH_Calculates.Instance.allLinks[i].pointAxisBlueCylinder.GetComponent<SetPositionRotation>().Rotating();
            DH_Calculates.Instance.allLinks[i].blueCylinder.transform.SetParent(curRedCylinder);
        }
    }
}
