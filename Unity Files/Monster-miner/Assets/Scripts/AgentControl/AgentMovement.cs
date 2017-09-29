//Daniel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class AgentMovement : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        UnitSelection.UnitList.Add(this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(Keybinds.Instance.SecondaryActionKey))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(ray,out hit))
            {
                if(hit.collider.tag == "Floor")
                {
                    Debug.Log("hit: " + hit.collider.name);
                    MoveToPoint(hit.point);
                }
            }
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }
}
