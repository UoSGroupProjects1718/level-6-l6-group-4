//Oliver
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BuildingManager : SingletonClass<BuildingManager>
{
    [SerializeField]
    private Job[] Buildings;
    [SerializeField]
    private float CreationDistance;
    [SerializeField]
    private Transform BuildingUIParent;
    [SerializeField]
    private GameObject ButtonPrefab;
    [SerializeField]
    private LayerMask FloorLayerMask;

    private BuildingFunction SelectedBuildingFunction;
    private GameObject CurrentlySelectedBuilding;
    private Job currentJob;

    private bool _hasPlaced;

    public bool HasPlaced
    {
        get
        {
            return _hasPlaced;
        }
    }

    private Vector3 BuildingLastPlaceHovered;

    public override void Awake()
    {
        base.Awake();
        //instantiate a button for each building in the array
        for (int i = 0; i < Buildings.Length; i++)
        {
            //instantiate a new button under the parent object, which holds a grid layout group
            GameObject button = Instantiate(ButtonPrefab, BuildingUIParent) as GameObject;
            //change the text component of the button (until we get an actual button to use as a prefab)
            button.GetComponentInChildren<Text>().text = (Buildings[i]).interactionObject.name;
            //then add an onclick event to the button using a delegate
            button.GetComponent<Button>().onClick.AddListener(delegate { BuildingOnClick(button.transform.GetSiblingIndex()); });
        }

    }

    private void Update()
    {

        if (CurrentlySelectedBuilding && !_hasPlaced)
        {
            //if we are mousing over ui, return
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            CurrentlySelectedBuilding.transform.position = BuildingLastPlaceHovered;

            //modify mouse position to world space
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseCastHit;
            if (Physics.Raycast(mousePos, out mouseCastHit, CreationDistance, FloorLayerMask.value))
            {
                BuildingLastPlaceHovered = mouseCastHit.point;
            }

            ///building rotation
            if (Input.GetKeyDown(Keybinds.Instance.BuildingRotationKey))
            {
                CurrentlySelectedBuilding.transform.Rotate(new Vector3(0, 90, 0));
            }

            ///cancel selection
            if (Input.GetKeyDown(Keybinds.Instance.CancelButton))
            {
                ResetBuildingSelections();
            }

            ///placement
            //if the primary action key is pressed
            if (Input.GetKeyDown(Keybinds.Instance.PrimaryActionKey) && IsLegalPosition())
            {
                //stop the building from following the mouse
                _hasPlaced = true;
                //and then we queue a job
                Job job = Instantiate(currentJob);
                job.jobLocation = CurrentlySelectedBuilding.transform.position;
                job.interactionObject = CurrentlySelectedBuilding;
                JobManager.Instance.QueueJob(job);
                CurrentlySelectedBuilding.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = true;
                //CurrentlySelectedBuilding.GetComponent<Collider>().enabled = true;
            }

        }

    }
    public void BuildingOnClick(int BuildingIndex)
    {
        _hasPlaced = false;
        CurrentlySelectedBuilding = Instantiate(Buildings[BuildingIndex].interactionObject);
        SelectedBuildingFunction = CurrentlySelectedBuilding.GetComponent<BuildingFunction>();
        //CurrentlySelectedBuilding.GetComponent<Collider>().enabled = false;
        CurrentlySelectedBuilding.GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
        currentJob = Buildings[BuildingIndex];
    }
    
    public bool IsLegalPosition()
    {
        return !(SelectedBuildingFunction.colliders.Count > 0);
    }

    private void ResetBuildingSelections()
    {
        _hasPlaced = false;
        Destroy(CurrentlySelectedBuilding);
        CurrentlySelectedBuilding = null;
        SelectedBuildingFunction = null;
        currentJob = null;
        BuildingLastPlaceHovered = Vector3.zero;
        Debug.Log("Reset Building Selections");

    }

}
