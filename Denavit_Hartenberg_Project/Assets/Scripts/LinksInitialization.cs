using System.Collections.Generic;
using UnityEngine;

public class LinksInitialization
{
    public void InitLinks(List<Link> links)
    {
        //Initialization
        if(links.Count > 1)
        {
            links[links.Count-1].blueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-2].redCylinder.transform.GetChild(1),null);

            //Ahora colocar las rotaciones de inicio
            links[links.Count-1].redCylinder.transform.rotation = Quaternion.Slerp(links[links.Count-1].redCylinder.transform.rotation, Quaternion.Euler(0f, links[links.Count-2].teta, 270), 1f);
            //links[links.Count-1].pointAxisRedCylinder.transform.rotation = Quaternion.Slerp(links[links.Count-1].pointAxisRedCylinder.transform.rotation, Quaternion.Euler(0f, links[links.Count-2].teta, 270), 1f);
        }
        links[links.Count-1].pointAxisBlueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].blueCylinder.transform, links[links.Count-1].blueCylinder.transform);

        links[links.Count-1].redCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].blueCylinder.transform.GetChild(1), null);
        links[links.Count-1].pointAxisRedCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].redCylinder.transform.GetChild(1), links[links.Count-1].redCylinder.transform);
    }
}
