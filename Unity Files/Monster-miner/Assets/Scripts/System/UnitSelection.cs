//Daniel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour {

    public static List<GameObject> UnitsSelected;
    public Vector3 Place;
    Vector3 startClick;
    Vector3 endClick;

    void Start()
    {
        UnitsSelected = new List<GameObject>();
    }
    void Update () {
        
        if (Input.GetKeyDown(Keybinds.Instance.PrimaryActionKey)) {
            if (!Input.GetKey(Keybinds.Instance.UnitMiltiSelect))//if not pressing multiselect
            {
                if(UnitsSelected.Count > 0)
                    UnitsSelected.Clear();//clear list
            }
            startClick = GetPoint();


        } ;
        if (Input.GetKeyUp(Keybinds.Instance.PrimaryActionKey))
        {
            endClick=GetPoint();
            //Draw a rect from start to end. add all agents to list
        }	
	}

    Vector3 GetPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
        //    Debug.Log(hit.point);
            return hit.point;
        }

        return new Vector3(0, 0, 0);
    }
}
