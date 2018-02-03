using System.Collections;
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
            GameObject newColonist = Instantiate(ColonistPrefab, ParentObj.transform);
            controllers.Add(newColonist.GetComponent<ColonistController>());
            newColonist.SetActive(false);
        }

        
    }

    public ColonistController SpawnColonist(Vector3 placement, ColonistJobType type)
    {
        ColonistController controller = GetController();
        controller.gameObject.transform.position = placement;
        controller.colonistEquipment = controller.GetComponent<Equipment>();
        controller.colonistJob = type;
        controller.EquipDefaultGear();
        controller.colonistName = GetName();

        return controller;
    }

    string GetName()
    {
        currentColonist++;
        return "Steve " + currentColonist.ToString();
    }

    ColonistController GetController()
    {
        foreach (ColonistController checkingController in controllers)
        {
            if (checkingController.enabled == false)
            {
                checkingController.gameObject.SetActive(true);
                return checkingController;
            }
        }


        GameObject gObject = Instantiate(ColonistPrefab, ParentObj.transform);
        ColonistController returnController = gObject.GetComponent<ColonistController>();
        controllers.Add(returnController);
        return returnController;
    }
    
}
