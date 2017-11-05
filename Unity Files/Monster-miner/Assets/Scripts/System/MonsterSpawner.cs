using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : SingletonClass<MonsterSpawner> {

    //Set up all lists
    List<MonsterController> controllers;
    List<List<GameObject>> MonsterLists;

    int numberPerList = 20;

    private void Start()
    {
        foreach (string key in MonsterTypes.Instance.dictionaryKeys) {
            GameObject currentMeshAndBones = MonsterTypes.Instance.Mons[key].monsterMeshAndBones;
            List<GameObject> workingList = new List<GameObject>();
            for (int i = 0; i < numberPerList; i++)
            {
                GameObject newMeshAndBones = Instantiate(currentMeshAndBones);
                newMeshAndBones.SetActive(false);
                workingList.Add(newMeshAndBones);
            }
            MonsterLists.Add(workingList);
        }
    }


    public void SpawnMonster(Vector3 placement, string type)
    {
        MonsterController controller = GetController();
        controller.gameObject.transform.position = placement;

        GetMesh(type, controller);

        MonsterTypes.Instance.getMonsterData(
           type, out controller.health, out controller.attackSpeed, out controller.damage,
           out controller.combatRange, out controller.attackSpeed, out controller.dropTable, out controller.matingCooldown, 
           out controller.numHunters, out controller.viewRange);
    }

    MonsterController GetController() {
        foreach (MonsterController checkingController in controllers)
        {
            if (checkingController.enabled == false)
            {
                return checkingController;
                
            }
        }
        MonsterController returnController = new MonsterController();
        controllers.Add(returnController);
        return returnController;
    }

    void GetMesh(string type, MonsterController parent)
    {
        GameObject wantedMeshAndBones = MonsterTypes.Instance.Mons[type].monsterMeshAndBones;

        foreach (List<GameObject> searchingList in MonsterLists)
        {
            if (searchingList[0] == wantedMeshAndBones)
            {
                //we have found the wanted list
                foreach(GameObject meshAndBones in searchingList)
                {
                    if (!meshAndBones.activeSelf) {
                        meshAndBones.SetActive(true);
                        meshAndBones.transform.position = parent.transform.position;
                        meshAndBones.transform.SetParent(parent.transform);
                        return;
                    }
                }
                GameObject createdMeshAndBones = Instantiate(wantedMeshAndBones, parent.transform);
                createdMeshAndBones.transform.SetParent(parent.transform);
                searchingList.Add(createdMeshAndBones);
                return;
            }
        }


        //find the type of monster
        //check the appropriate list
        //if not found usable mesh and bones, create a new one
        //active mesh & bones
        //set parent as MonsterController


    }

}
