using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRotation : MonoBehaviour
{
    public Transform pointAxis;
    public Transform rotationAxis;


    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pointAxis.position;

        //oldEulerAngles = rotationAxis.rotation.eulerAngles;
        
    }

    public void Rotating()
    {
        transform.SetParent(rotationAxis);
    }

    public void FinishRotation()
    {
        transform.SetParent(null);
    }
}
