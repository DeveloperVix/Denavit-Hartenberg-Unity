using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
    public class Link
    {
        public string nameLink;
        public GameObject blueCylinder;
        public GameObject pointAxisBlueCylinder;
        public GameObject redCylinder;
        public GameObject pointAxisRedCylinder;

        
        public float teta;
        public float d;
        public float a;
        public float delta;
    }

public class LinksStructure : MonoBehaviour
{
    public TMP_Dropdown optionsLink;
    List<string> options = new List<string>();

    /*
        0 = blueCylinder
        1 = pointAxis
        2 = redCylinder
    */
    public GameObject[] prefabs;

    LinksInitialization initLinks = new LinksInitialization();
    
    private void Start()
    {
        optionsLink.ClearOptions();
    }

    //Method called from the button "Añadir nuevo enlace"
    public void CreateNewLink()
    {
        Link newLink = new Link();

        newLink.blueCylinder = Instantiate(prefabs[0], new Vector3(0,0,0), Quaternion.identity);
        
        newLink.pointAxisBlueCylinder = Instantiate(prefabs[1], new Vector3(0,0,0), Quaternion.identity);

        newLink.redCylinder = Instantiate(prefabs[2], new Vector3(0,0,0), Quaternion.identity);
        newLink.pointAxisRedCylinder = Instantiate(prefabs[1], new Vector3(0,0,0), Quaternion.identity);
        

        DH_Calculates.Instance.allLinks.Add(newLink);
        newLink.nameLink = "Nodo " + (DH_Calculates.Instance.allLinks.Count-1);
        options.Add(newLink.nameLink);
        optionsLink.ClearOptions();
        optionsLink.AddOptions(options);

        initLinks.InitLinks(DH_Calculates.Instance.allLinks);        
    }

}


