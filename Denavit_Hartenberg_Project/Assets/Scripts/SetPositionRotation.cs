using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPositionRotation : MonoBehaviour
{
    public Transform pointAxis;
    public Transform rotationAxis;

    // Update is called once per frame
    void Update()
    {
        if(pointAxis != null)
            transform.position = pointAxis.position;        
    }

    public void InitPositionRotation(Transform pointAxis, Transform rotationAxis)
    {
        this.pointAxis = pointAxis;
        this.rotationAxis = rotationAxis;
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
