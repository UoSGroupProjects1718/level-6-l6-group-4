using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [Header("Buildings")]
    public GameObject Blacksmith;
    public GameObject Field;
    public GameObject Stockpile;


    [Header("Jobs")]
    public Job BlacksmithJob;
    public Job FieldJob;
    public Job StockpileJob;


    private CreateBuilding createBuilding;

	void Start ()
    {
        createBuilding = GetComponent<CreateBuilding>();
	}
	
    public void BlacksmithOnClick()
    {
        createBuilding.SetItem(Blacksmith,BlacksmithJob);
        
    }
    public void FieldOnClick()
    {
        createBuilding.SetItem(Field,FieldJob);
    }

    public void StockpileOnClick()
    {
        createBuilding.SetItem(Stockpile, StockpileJob);
    }

}
