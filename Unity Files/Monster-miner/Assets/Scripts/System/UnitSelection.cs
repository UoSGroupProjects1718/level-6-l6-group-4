//Daniel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour {

    public static List<GameObject> UnitsSelected;
    public static List<Transform> UnitList;
    public Vector3 Place;
    Vector3 startClick;
    Vector3 endClick;

    void Awake()
    {
        UnitsSelected = new List<GameObject>();
        UnitList = new List<Transform>();
    }
    void Update () {
        
        if (Input.GetKeyDown(Keybinds.Instance.PrimaryActionKey)) {

            startClick = GetPoint();

            if (!Input.GetKey(Keybinds.Instance.UnitMiltiSelect) && startClick != new Vector3(0,0,0))//if not pressing multiselect
            {
                if(UnitsSelected.Count > 0)
                    UnitsSelected.Clear();//clear list
            }
            


        } 
        if (Input.GetKeyUp(Keybinds.Instance.PrimaryActionKey))
        {
            endClick=GetPoint();
            //Draw a rect from start to end. add all agents to list
            AddAgentsToList();
            Debug.Log(UnitsSelected);
        }	
	}

    Vector3 GetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.point);
            return hit.point;
        }

        return new Vector3(0, 0, 0);
    }

    void AddAgentsToList()
    {
        if(startClick==new Vector3(0,0,0) || endClick == new Vector3(0, 0, 0))//if either point is invalid
        {
            return;
        }

        float lowx, lowz, highx, highz;

        lowx = Mathf.Min(startClick.x, endClick.x);
        lowz = Mathf.Min(startClick.z, endClick.z);

        highx = Mathf.Max(startClick.x, endClick.x);
        highz = Mathf.Max(startClick.z, endClick.z);

        for (int i = 0; i < UnitList.Count; i++)
        {
            Transform thisUnit = UnitList[i];
            if (
                lowx < thisUnit.position.x && thisUnit.position.x < highx &&
                lowz < thisUnit.position.z && thisUnit.position.z < highz &&
                !UnitsSelected.Contains(thisUnit.gameObject)
                )
            {
                UnitsSelected.Add(thisUnit.gameObject);
            }
        }
    }

    
}
