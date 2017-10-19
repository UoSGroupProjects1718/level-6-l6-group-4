////Daniel
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class UnitOrder : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//        if (Input.GetKey(Keybinds.Instance.SecondaryActionKey))
//        {
//            GameObject hitObject = null;

//             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit))
//            {
//                Debug.Log(hit.point);
//                hitObject = hit.collider.gameObject;//
//            }

//            //do checks for what we hit here
//        }
//	}

//    void MakeAgentsMove(Vector3 Point)
//    {
//        for (int i = 0; i < UnitSelection.UnitsSelected.Count; i++) {//go through all selected agents
//            UnitSelection.UnitsSelected[i].GetComponent<AgentMovement>().MoveToPoint(Point);//move them to this point
//        }
//    }
//}
