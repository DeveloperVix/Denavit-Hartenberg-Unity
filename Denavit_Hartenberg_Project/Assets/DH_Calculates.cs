using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Calculates : MonoBehaviour
{
    private static DH_Calculates instance;
    public static DH_Calculates Instance { get => instance;}
    public List<Link> allLinks;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
