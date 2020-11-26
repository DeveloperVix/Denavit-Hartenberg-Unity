using System.Collections.Generic;

public class LinksInitialization
{
    public void InitLinks(List<Link> links)
    {
        //Initialization
        if(links.Count > 1)
        {
            links[links.Count-1].blueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-2].redCylinder.transform.GetChild(1),null);
        }
        links[links.Count-1].pointAxisBlueCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].blueCylinder.transform, links[links.Count-1].blueCylinder.transform);

        links[links.Count-1].redCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].blueCylinder.transform.GetChild(1), null);
        links[links.Count-1].pointAxisRedCylinder.GetComponent<SetPositionRotation>().InitPositionRotation(links[links.Count-1].redCylinder.transform.GetChild(1), links[links.Count-1].redCylinder.transform);

        //Ahora colocar las rotaciones de inicio
    }
}
