using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;

public class ColonistSpawner : SingletonClass<ColonistSpawner>
{
    

    //Set up all lists
    List<ColonistController> controllers;
    GameObject ParentObj;
    public GameObject ColonistPrefab;

    int defaultNumberOfColonists = 20;
    int currentColonist =0;


    public void SpawnColonistLists()
    {
        ParentObj = new GameObject();
        ParentObj.name = "ColonistPoolParentObj";
        ParentObj.transform.position = Vector3.zero;
        ParentObj.transform.rotation = Quaternion.identity;

        
        controllers = new List<ColonistController>();
        for (int i = 0; i < defaultNumberOfColonists; i++)
        {
            NavMeshHit navHit;
            if (NavMesh.SamplePosition(Vector3.zero, out navHit, 10f, -1))
            {
                GameObject newColonist = Instantiate(ColonistPrefab,navHit.position,Quaternion.identity, ParentObj.transform);
                controllers.Add(newColonist.GetComponent<ColonistController>());
                newColonist.SetActive(false);
            }
            else
            {
                Debug.LogError("Colonist pool spawning failed at " + i);
            }
        }

        
    }

    public ColonistController SpawnColonist(Vector3 placement, ColonistJobType type)
    {
        ColonistController controller = GetController();
        controller.gameObject.transform.position = placement;
        controller.gameObject.transform.rotation = Quaternion.identity;
        controller.colonistEquipment = controller.GetComponent<Equipment>();
        controller.colonistJob = type;
        controller.EquipDefaultGear();
        controller.colonistName = GetName();

        return controller;
    }

    //return colonist back to pool
    public void ReturnColonistToPool(GameObject colonist)
    {
        colonist.SetActive(false);
    }


    string GetName()
    {
        currentColonist++;
        return "Steve " + currentColonist.ToString();
    }

    //find an inactive controller
    ColonistController GetController()
    {
        foreach (ColonistController checkingController in controllers)
        {
            if (checkingController.gameObject.activeSelf == false)
            {
                checkingController.gameObject.SetActive(true);
                return checkingController;
            }
        }

        NavMeshHit navHit;
        if (NavMesh.SamplePosition(Vector3.zero, out navHit, 10f, -1))
        {
            GameObject gObject = Instantiate(ColonistPrefab, ParentObj.transform);
            ColonistController returnController = gObject.GetComponent<ColonistController>();
            controllers.Add(returnController);
            return returnController;
        }
        Debug.LogError("Colonist could not be spawned");
        return null;
    }
    
}
