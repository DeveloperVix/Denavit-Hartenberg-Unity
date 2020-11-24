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

    public Transform redCylinder;

    [Header("A Slider")]
    public Slider aSlider;
    float previousValueA;
    public InputField inputA;

    [Header("Delta Slider")]
    public Slider deltaSlider;
    public float prevoiusValueDeltaOnX;
    float previousValueDeltaOnY;
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
            redCylinder.rotation = Quaternion.Slerp(redCylinder.rotation, Quaternion.Euler(0, 0, 0), 1f);
        }

    }

    public void PrismaticType()
    {
        if(prismaticType.isOn)
        {
            rotationalType.isOn = false;
            redCylinder.rotation = Quaternion.Slerp(redCylinder.rotation, Quaternion.Euler(0, 0, 270), 1f);
        }
    }


    public void ControlA(float value)
    {
        float a = value - previousValueA;
        redCylinder.localScale += Vector3.up * a;
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
        inputDelta.text = redCylinder.localEulerAngles.y.ToString();
    }

    public void ControlDelta(float value)
    {
        //Debug.Log("Modifico Rotación");
        //Debug.Log(redCylinder.rotation);
        //Debug.Log(redCylinder.eulerAngles);

        /*if(redCylinder.eulerAngles.z == 270)
        {
            float delta = value - prevoiusValueDeltaOnX;
            Debug.Log("Roto");
            redCylinder.Rotate(new Vector3(0f, delta * 360, 0f), Space.Self);// Vector3.down * delta * 360);
            prevoiusValueDeltaOnX = value;
        }
        else
        {*/

        if (rotationalType.isOn)
        {
            Debug.Log("Modifico Rotación");
            redCylinder.rotation = Quaternion.Slerp(redCylinder.rotation, Quaternion.Euler(0, value, 0), 1f);
        }
        else if (prismaticType.isOn)
        {
            Debug.Log("Modifico Rotación acostado");
            redCylinder.rotation = Quaternion.Slerp(redCylinder.localRotation, Quaternion.Euler(value, 0, 270), 1f);
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
