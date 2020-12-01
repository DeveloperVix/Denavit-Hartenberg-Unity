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
        public GameObject teta_d_Slider;

        
        public float teta;
        public float d;
        public float a;
        public float alfa;
    }

public class LinksStructure : MonoBehaviour
{
    public TMP_Dropdown optionsLink;
    List<string> options = new List<string>();

    /*
        0 = blueCylinder
        1 = pointAxis
        2 = redCylinder
        3 = teta D slider
    */
    public GameObject[] prefabs;

    public GameObject theCanvas;
    
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

        newLink.redCylinder = Instantiate(prefabs[2], new Vector3(0,0,0), Quaternion.Euler(0, 0, 270));
        newLink.pointAxisRedCylinder = Instantiate(prefabs[1], new Vector3(0,0,0), Quaternion.identity);

        newLink.teta_d_Slider = Instantiate(prefabs[3]);
        newLink.teta_d_Slider.transform.SetParent(theCanvas.transform);

        newLink.redCylinder.transform.SetParent(null);
        newLink.redCylinder.transform.localScale = new Vector3(1,0,1);
        newLink.pointAxisRedCylinder.transform.SetParent(null);
        newLink.pointAxisRedCylinder.transform.localScale = new Vector3(1,1,1);
        newLink.blueCylinder.transform.SetParent(null);
        newLink.blueCylinder.transform.localScale = new Vector3(1,0,1);
        newLink.pointAxisBlueCylinder.transform.SetParent(null);
        newLink.pointAxisBlueCylinder.transform.localScale = new Vector3(1,1,1);
        
        DH_Calculates.Instance.allLinks.Add(newLink);
        newLink.nameLink = "Nodo " + (DH_Calculates.Instance.allLinks.Count-1);
        options.Add(newLink.nameLink);
        optionsLink.ClearOptions();
        optionsLink.AddOptions(options);

        initLinks.InitLinks(DH_Calculates.Instance.allLinks);        
    }

}


