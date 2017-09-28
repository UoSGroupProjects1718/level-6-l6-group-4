using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Jobs/BuildJob")]
public class BuildJob : Job {

    [HideInInspector]
    public GameObject building;

    [HideInInspector]
    public PlaceableBuilding BF;

    public override void Initialise()
    {
        BF = building.GetComponent<PlaceableBuilding>();
        BF.enabled = false;
    }

    public override void OnJobComplete()
    {
        //remove this from the docket if it's completed
        GlobalBlackboard.JobDocket.Remove(this);
        //then reset its colour
        building.GetComponent<SpriteRenderer>().color = Color.white;
        building.GetComponent<Collider2D>().enabled = true;
        //then create the avoid grid
        CreateAvoidGrid.GetInstance().CheckNodes(building.transform.position);
        //then find the building function
        //and enable it 
        if(BF != null)
        {
            BF.built = true;
            BF.enabled = true;
        }
    }
}
