using UnityEngine;

public class CreateBuilding : MonoBehaviour {

    public LayerMask buildingMask;

    [Header("Keybinds")]
    public KeyCode placeBuildingKey = KeyCode.Mouse0;
    public KeyCode rotateBuildingLeft = KeyCode.Q;
    public KeyCode rotateBuildingRight = KeyCode.E;
    public KeyCode cancelBuildingPlacement = KeyCode.Escape;
    



    [Header("Tint Color")]
    [SerializeField]
    private Color CannotPlaceTint = Color.red;
    [SerializeField]
    private Color UnbuiltTint = Color.grey;
    
    private SpriteRenderer buildingSpriteRenderer; 
    private Transform currentBuilding;
    private bool hasPlaced;
    private Grid grid;
    private PlaceableBuilding placeableBuilding;
    private BuildJob newJob;

    void Start () {
        grid = GameObject.Find("Pathfinding").GetComponent<Grid>();
	}
	
	void Update ()
    {
        placeBuilding();  
	}
    void placeBuilding()
    {
        //if the building isnt null and hasnt been placed
        if (currentBuilding != null && !hasPlaced)
        {
            //get the mouse position
            Vector3 mousePos = Input.mousePosition;
            //modify it to world space
            Vector3 worldSpace = Camera.main.ScreenToWorldPoint(mousePos);
            //then set the building's central position to the node
            Node node = grid.NodeFromWorldPoint(worldSpace);
            //then set the buildings central position to the node
            currentBuilding.position = node.worldPosition;
            ///BAD CODE /////////////////////////////////////////
            //if it is not a legal position then tint it
            if (!IsLegalPosition())
            {
                buildingSpriteRenderer.color = CannotPlaceTint;
            }
            else
            {
                buildingSpriteRenderer.color = Color.white;
            }
          
            ///////////////////////////////////////////////////////
            //if we press rotate left, rotate the selection left 90 degrees
            if (Input.GetKeyDown(rotateBuildingRight))
            {
                //store a temporary float for the buildings x size
                float buildingSizeX = placeableBuilding.BuildingSize.x;
                //swap the current x for the buildings y dimension
                placeableBuilding.BuildingSize.x = placeableBuilding.BuildingSize.y;
                //then swap the y dimension for the old x dimension
                placeableBuilding.BuildingSize.y = buildingSizeX;

                currentBuilding.Rotate(new Vector3(0, 0, -90));
            }

            //or if rotate right is pressed, rotate the selection right by 90 degrees
            if (Input.GetKeyDown(rotateBuildingLeft))
            {
                //store a temporary float for the buildings x size
                float buildingSizeX = placeableBuilding.BuildingSize.x;
                //swap the current x for the buildings y dimension
                placeableBuilding.BuildingSize.x = placeableBuilding.BuildingSize.y;
                //then swap the y dimension for the old x dimension
                placeableBuilding.BuildingSize.y = buildingSizeX;

                currentBuilding.Rotate(new Vector3(0, 0, 90));
            }

            //if the cancel key is pressed
            if (Input.GetKeyDown(cancelBuildingPlacement))
            {
                //destroy the current building and make sure both building variables are null
                Destroy(currentBuilding.gameObject);
                currentBuilding = null;
                placeableBuilding = null;
                newJob = null;
                return;
            }

            //if the place key is pressed
            if (Input.GetKeyDown(placeBuildingKey))
            {
                //and the position is legal
                if (IsLegalPosition())
                {
                    //set hasplaced to true
                    hasPlaced = true;
                    //set the sprite render's colour to the unbuilt tint
                    buildingSpriteRenderer.color = UnbuiltTint;
                    //create a new job and add it to the list with the create job function.
                    //telling it to go to an interaction spot object (empty object that must always be child number 1) can change this at a later date
                    newJob.jobLocation = currentBuilding.transform.position;
                    //and we need to set the building for it to be referencing
                    newJob.building = currentBuilding.gameObject;
                    newJob.Initialise();
                    JobManager.instance.CreateJob(newJob);
                }
            }
        }
    }
    public void SetItem(GameObject building,Job job)
    {
        //set hasplaced to false
        hasPlaced = false;
        //set the current building
        currentBuilding = (Instantiate(building)).transform;
        //set the sprite renderer
        buildingSpriteRenderer = currentBuilding.GetComponent<SpriteRenderer>();
        //then set placeable building to the script on the current building
        placeableBuilding = currentBuilding.GetComponent<PlaceableBuilding>();
        //now get the new job from the manager which corresponds to the building, to be queued when the create button is clicked
        newJob = Instantiate((BuildJob)job);
    }
    private bool IsLegalPosition()
    {
        if (placeableBuilding.colliders.Count > 0)
        {
            return false;
        }
        return true;
    }
}
