using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : SingletonClass<MonsterSpawner> {

    //Set up all lists
    List<MonsterController> controllers;
    List<List<GameObject>> monsterLists;
    GameObject parentObj;

    int numberPerList = 20;

    public void SpawnMonsterLists()
    {
        parentObj = new GameObject();
        parentObj.name = "MonsterPoolParentObj";
        parentObj.transform.position = Vector3.zero;
        parentObj.transform.rotation = Quaternion.identity;


        monsterLists = new List<List<GameObject>>();
        controllers = new List<MonsterController>();
        foreach (string key in MonsterTypes.Instance.dictionaryKeys)
        {
            try
            {
                GameObject currentMeshAndBones = MonsterTypes.Instance.Mons[key].monsterMeshAndBones;
                List<GameObject> workingList = new List<GameObject>();
                for (int i = 0; i < numberPerList; i++)
                {
                    GameObject newMeshAndBones = Instantiate(currentMeshAndBones);
                    newMeshAndBones.SetActive(false);
                    workingList.Add(newMeshAndBones);
                    newMeshAndBones.transform.SetParent(parentObj.transform);
                }
                monsterLists.Add(workingList);
            }
            catch { Debug.Log("No mesh"); }
        }
    }

    public void NewWorldSpawnMonsters()
    {
        for (int i = 0; i < 15; i++)
        {
            SpawnMonster(Utils.RandomNavSphere(Vector3.zero, 90, -1), MonsterTypes.Instance.dictionaryKeys[Random.Range(0, 24)]);
        }
    }

    public MonsterController SpawnMonster(Vector3 placement, string type)
    {
        MonsterController controller = GetController();
        controller.gameObject.transform.position = placement;
        GetMesh(type, controller);

        MonsterTypes.Instance.getMonsterData(
           type, out controller.maxHealth, out controller.maxHunger,out controller.hungerAttackPercentage ,out controller.hungerLossPerSecond, out controller.hungerDamage, out controller.naturalRegen, out controller.monsterSpeed, out controller.damage,
           out controller.combatRange, out controller.attackSpeed, out controller.dropTable, out controller.matingCooldown, 
           out controller.numHunters, out controller.viewRange, out controller.monsterType);
        controller.gameObject.name = type;
        controller.GetMonster();
        return controller;
    }

    MonsterController GetController() {
        foreach (MonsterController checkingController in controllers)
        {
            if (checkingController.enabled == false)
            {
                return checkingController;
                
            }
        }
        GameObject gObject = new GameObject();
        MonsterController returnController = gObject.AddComponent(typeof(MonsterController)) as MonsterController;
        gObject.AddComponent(typeof(MonsterMovement));
        gObject.transform.SetParent(parentObj.transform);
        controllers.Add(returnController);
        return returnController;
    }

    void GetMesh(string type, MonsterController parent)
    {
        GameObject wantedMeshAndBones = MonsterTypes.Instance.Mons[type].monsterMeshAndBones;

        foreach (List<GameObject> searchingList in monsterLists)
        {
            if (searchingList[0].name == wantedMeshAndBones.name +"(Clone)")
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
                createdMeshAndBones.transform.GetChild(createdMeshAndBones.transform.childCount-1).transform.localEulerAngles = new Vector3(0,0,90);
                createdMeshAndBones.transform.SetParent(parent.transform);
                searchingList.Add(createdMeshAndBones);
                return;
            }
        }

        Debug.Log("THERE IS NO DINO YOU ARE LOOKING FOR");


        //find the type of monster
        //check the appropriate list
        //if not found usable mesh and bones, create a new one
        //active mesh & bones
        //set parent as MonsterController


    }

}
