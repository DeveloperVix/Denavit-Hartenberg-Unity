using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RoboticArm : MonoBehaviour
{
    [Header("Type rotation")]
    public Toggle rotationalType;
    public Toggle prismaticType;

    public Transform curRedCylinder;

    [Header("A Slider")]
    public Slider aSlider;
    float previousValueA;
    public InputField inputA;

    [Header("Delta Slider")]
    public Slider deltaSlider;
    public InputField inputDelta;

    // Start is called before the first frame update
    void Start()
    {
        aSlider.onValueChanged.AddListener(ControlA);
        deltaSlider.onValueChanged.AddListener(ControlDelta);
        inputA.text = "0";
        inputDelta.text = "0";
    }

    public void RotationalType()
    {
        if (rotationalType.isOn)
        {
            prismaticType.isOn = false;
            curRedCylinder.rotation = Quaternion.Slerp(curRedCylinder.rotation, Quaternion.Euler(0, 0, 0), 1f);
        }

    }

    public void PrismaticType()
    {
        if(prismaticType.isOn)
        {
            rotationalType.isOn = false;
            curRedCylinder.rotation = Quaternion.Slerp(curRedCylinder.rotation, Quaternion.Euler(0, 0, 270), 1f);
        }
    }


    public void ControlA(float value)
    {
        float a = value - previousValueA;
        curRedCylinder.localScale += Vector3.up * a;
        previousValueA = value;
        
    }

    //Called from the A input field event
    public void SetInputA()
    {
        aSlider.value = float.Parse(inputA.text);
    }

    //Called from the A slider event
    public void InputA()
    {
        inputA.text = aSlider.value.ToString();
        inputDelta.text = curRedCylinder.localEulerAngles.y.ToString();
    }

    public void ControlDelta(float value)
    {
        if (rotationalType.isOn)
        {
            Debug.Log("Modifico Rotación");
            curRedCylinder.rotation = Quaternion.Slerp(curRedCylinder.rotation, Quaternion.Euler(0, value, 0), 1f);
        }
        else if (prismaticType.isOn)
        {
            Debug.Log("Modifico Rotación acostado");
            curRedCylinder.rotation = Quaternion.Slerp(curRedCylinder.localRotation, Quaternion.Euler(value, 0, 270), 1f);
        }

        //}
    }

    //Called from the Delta input field event
    public void SetInputDelta()
    {
        deltaSlider.value = float.Parse(inputDelta.text);
    }

    //Called from the delta slider event
    public void InputDelta()
    {
        if(deltaSlider.value == 0)
        {
            inputDelta.text = "0";
        }
        else
        {
            inputDelta.text = deltaSlider.value.ToString();
        }
    }
}
