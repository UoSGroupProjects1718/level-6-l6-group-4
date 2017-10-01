using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PrototypeBehaviour : MonoBehaviour {

    public enum State
    {
        Wander,
        Flee
    }
    public float range = 10;
    public float radaius = 5;

    public State currentState = State.Wander;

    NavMeshAgent navMeshAgent;
    
	// Use this for initialization
	void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Transform ClosestEnemy = null;
        if(CheckNearbby(out ClosestEnemy))
        {
            Flee(ClosestEnemy);
        }
        else {
            Wander();
        }
	}

    bool CheckNearbby(out Transform returnTransform)
    {
        Transform ClosestEnemy = null;
        float Close = float.MaxValue;
        for(int i=0; i<UnitSelection.UnitList.Count; i++){
            float dist = (UnitSelection.UnitList[i].position - transform.position).magnitude;
            
            if(dist < range && dist < Close) {
                ClosestEnemy = UnitSelection.UnitList[i];
                Close = dist;
            }
        }
        returnTransform = ClosestEnemy;
        if (ClosestEnemy == null)
        {
            currentState = State.Wander;
            return false;
        }
        currentState = State.Flee;
        return true;
    }


    void Flee(Transform agentPosition)
    {
        Vector3 Target = 2* transform.position - new Vector3(agentPosition.position.x,agentPosition.position.y,agentPosition.position.z);
        MoveToPoint(Target);
    }

    void Wander()
    {
        circleCentre = transform.forward.normalized * 5 + transform.position;
        float Angle = Random.Range(0, 2*Mathf.PI);
        Vector3 Target = new Vector3(circleCentre.x + radaius * Mathf.Sin(Angle), circleCentre.y, radaius * circleCentre.z + Mathf.Cos(Angle));
        MoveToPoint(Target);
        Debug.Log(Target);
            }

    public void MoveToPoint(Vector3 point)
    {
        navMeshAgent.SetDestination(point);
    }

    Vector3 circleCentre;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(circleCentre, radaius);
        Gizmos.DrawLine(transform.position, transform.position + transform.forward);
    }
}
