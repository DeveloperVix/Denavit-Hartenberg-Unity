using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinksStructure : MonoBehaviour
{
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

    public TMP_Dropdown optionsLink;
    List<string> options = new List<string>();

    /*
        0 = blueCylinder
        1 = pointAxis
        2 = redCylinder
    */
    public GameObject[] prefabs;
    public List<Link> allLinks;
    
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
        

        allLinks.Add(newLink);
        newLink.nameLink = "Nodo " + (allLinks.Count-1);
        options.Add(newLink.nameLink);

        //Initialization
        if(allLinks.Count > 1)
        {
            newLink.blueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(allLinks[allLinks.Count-2].redCylinder.transform.GetChild(1),null);
        }
        newLink.pointAxisBlueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(newLink.blueCylinder.transform, newLink.blueCylinder.transform);

        newLink.redCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(newLink.blueCylinder.transform.GetChild(1), null);
        newLink.pointAxisRedCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(newLink.redCylinder.transform.GetChild(1), newLink.redCylinder.transform);
        
        optionsLink.ClearOptions();
        optionsLink.AddOptions(options);
        //Ahora colocar las rotaciones de inicio
    }

}


