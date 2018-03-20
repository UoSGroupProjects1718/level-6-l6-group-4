using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

/*
 
     JOB NEEDS TO BE SERIALIZABLE, 
     GameObject needs to be changed to the name of the object 
     THEN THIS WILL WORK
     //SAVE DROPS
     */

public class SaveData : MonoBehaviour {
    string path = "./";

    #region Save
    public void Save()
    {
        Dictionary<string, object> saveData = new Dictionary<string, object>();
        
        #region MonsterData
        List<MonsterData> monsterData = new List<MonsterData>();

        for (int i = 0; i < BehaviourTreeManager.Monsters.Count; i++)
        {
            if (BehaviourTreeManager.Monsters[i].isActiveAndEnabled)
            {
                monsterData.Add(SaveMonsterData(BehaviourTreeManager.Monsters[i]));
            }
        }
        saveData.Add("Monsters", monsterData);

        string[] monsterKeys = MonsterTypes.Instance.dictionaryKeys.ToArray();
        List<DictionaryEntries> numHunters = new List<DictionaryEntries>();
        for (int i = 0; i < MonsterTypes.Instance.Mons.Keys.Count; i++)
        {
            DictionaryEntries data = new DictionaryEntries();
            data.key = monsterKeys[i];
            data.value = MonsterTypes.Instance.Mons[monsterKeys[i]].numHuntersRequired;
            numHunters.Add(data);
        }
        saveData.Add("NumberOfHunters", numHunters);

        #endregion
        #region ColonistData

        
        List<ColonistData> colonistData = new List<ColonistData>();

        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            if (BehaviourTreeManager.Colonists[i].isActiveAndEnabled)
            {
                colonistData.Add(SaveColonistData(BehaviourTreeManager.Colonists[i]));
            }
        }
        
        //saveData.Add("Colonists", colonistData);
        #endregion
        #region TerrainData
        GameObject terrainParent = GameObject.Find("Terrain");
        List<TerrainData> terrain = new List<TerrainData>();
        if(terrainParent!=null)
        for (int i = 0; i < terrainParent.transform.childCount; i++)
        {
            string name = terrainParent.transform.GetChild(i).name;
            string[] test = name.Split('(');
            string newName = test[0];   

            TerrainData terrainData = new TerrainData();
            terrainData.gameObject = terrainParent.transform.GetChild(i).gameObject;
            terrainData.pos.x = terrainParent.transform.GetChild(i).position.x;
            terrainData.pos.y = terrainParent.transform.GetChild(i).position.y;
            terrainData.pos.z = terrainParent.transform.GetChild(i).position.z;
            terrainData.rot.x = terrainParent.transform.GetChild(i).rotation.eulerAngles.x;
            terrainData.rot.y = terrainParent.transform.GetChild(i).rotation.eulerAngles.y;
            terrainData.rot.z = terrainParent.transform.GetChild(i).rotation.eulerAngles.z;
            terrain.Add(terrainData);
        }

        //saveData.Add("Terrain", terrain);
        #endregion
        #region BuildingData
        List<Building> buildings = SaveBuildings();

        saveData.Add("Buildings", buildings);
        #endregion
        #region Time
        MyGameTime saveTime = new MyGameTime();
        
        
        saveData.Add("Time", saveTime);

        #endregion
        #region Rescources
        KeyValuePair<ItemType,int>[] rescourceEntries = Stockpile.Instance.inventoryDictionary.ToArray();
        
        saveData.Add("RescourceEntries", rescourceEntries);


        KeyValuePair<Wearable, int>[] wearableRescourceEntries = Stockpile.Instance.wearableInventoryDictionary.ToArray();

        saveData.Add("WearableRescourceEntries", wearableRescourceEntries);
        #endregion

        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Create(path + "SaveData.dat");
        Debug.Log(saveData);
        bF.Serialize(file, saveData);
        file.Close();

        Debug.Log("Save Complete");
    }

    MonsterData SaveMonsterData(MonsterController monster)
    {
        MonsterData data = new MonsterData();
        data.beingHunted = monster.beingHunted;
        data.health = monster.health;
        data.hunger = monster.hunger;
        data.lastMatingTime = monster.lastMatingTime;
        data.monsterType = monster.gameObject.name;
        data.Pos.x = monster.transform.position.x;
        data.Pos.y = monster.transform.position.y;
        data.Pos.z = monster.transform.position.z;
        data.Rot.x = monster.transform.rotation.eulerAngles.x;
        data.Rot.y = monster.transform.rotation.eulerAngles.y;
        data.Rot.z = monster.transform.rotation.eulerAngles.z;
        return data;
    }

    ColonistData SaveColonistData(ColonistController colonist)
    {
        ColonistData data = new ColonistData();
        //save transform position
        data.Pos.x = colonist.transform.position.x;
        data.Pos.y = colonist.transform.position.y;
        data.Pos.z = colonist.transform.position.z;
        //save transform rotation
        data.Rot.x = colonist.transform.rotation.eulerAngles.x;
        data.Rot.y = colonist.transform.rotation.eulerAngles.y;
        data.Rot.z = colonist.transform.rotation.eulerAngles.z;
        //save equipped gear
        data.torso = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Torso].itemName;
        data.legs = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Legs].itemName;
        data.weapon = colonist.colonistEquipment.weapon.itemName;
        //save colonist's profession
        data.colonistJob = colonist.colonistJob;
        //save colonist job (if one exists)
        data.currentJob = SaveColonistJob(colonist.currentJob);
        //save name
        data.colonistName = colonist.colonistName;
        //save current hp
        data.health = colonist.health;
        //save the last time the colonist contributed to a job
        data.lastWorked.hours.x = colonist.timeOfNextMeal.hours;
        data.lastWorked.hours.y = colonist.timeOfNextMeal.minutes;
        data.lastWorked.Date.x = colonist.timeOfNextMeal.Date.x;
        data.lastWorked.Date.y = colonist.timeOfNextMeal.Date.y;
        data.lastWorked.Date.z = colonist.timeOfNextMeal.Date.z;
        //save the next time the colonist needs to eat
        MyGameTime nextMeal = new MyGameTime();
        nextMeal.hours.x = colonist.timeOfNextMeal.hours;
        nextMeal.hours.y = colonist.timeOfNextMeal.minutes;
        nextMeal.Date.x = colonist.timeOfNextMeal.Date.x;
        nextMeal.Date.y = colonist.timeOfNextMeal.Date.y;
        nextMeal.Date.z = colonist.timeOfNextMeal.Date.z;

        data.timeOfNextMeal = nextMeal;
        return data;
    }

    MyJob SaveColonistJob(Job currentJob)
    {
        MyJob returnJob = new MyJob();
        if(currentJob != null)
        {
            //curr work amount
            returnJob.currentWorkAmount = currentJob.currentWorkAmount;
            //interaction ITEM name
            returnJob.interactionItemName = currentJob.interactionItem.itemName;
            //interaction OBJECT name
            returnJob.interactionObjectName = currentJob.interactionObject.name;
            //job location
            returnJob.jobLocation.x = currentJob.jobLocation.x;
            returnJob.jobLocation.x = currentJob.jobLocation.y;
            returnJob.jobLocation.x = currentJob.jobLocation.z;
            //job type
            returnJob.jobType = currentJob.jobType.ToString();
            //job name
            returnJob.jobName = currentJob.jobName;
            //required items;
            MyRequiredItem[] requiredItems = new MyRequiredItem[currentJob.RequiredItems.Length];
            for(int i = 0; i < currentJob.RequiredItems.Length;i++)
            {
                MyRequiredItem requiredItem = new MyRequiredItem();
                requiredItem.resourceName = currentJob.RequiredItems[i].resource.ToString();
                requiredItem.requiredAmount = currentJob.RequiredItems[i].requiredAmount;
                requiredItems[i] = requiredItem;
            }
            returnJob.requiredItems = requiredItems;

        }

        return returnJob;
    }

    List<Building> SaveBuildings()
    {
        List<Building> buildings = new List<Building>();
        #region Armouries
        //armoury
        for (int i = 0; i < BehaviourTreeManager.Armouries.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Armouries[i].gameObject;
            building.pos.x = BehaviourTreeManager.Armouries[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Armouries[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Armouries[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Armouries[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Armouries[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Armouries[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        #region Barracks
        for (int i = 0; i < BehaviourTreeManager.Barracks.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Barracks[i].gameObject;
            building.pos.x = BehaviourTreeManager.Barracks[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Barracks[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Barracks[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Barracks[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Barracks[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Barracks[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        #region BlackSmith
        for (int i = 0; i < BehaviourTreeManager.Blacksmiths.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Blacksmiths[i].gameObject;
            building.pos.x = BehaviourTreeManager.Blacksmiths[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Blacksmiths[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Blacksmiths[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Blacksmiths[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Blacksmiths[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Blacksmiths[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        #region Granary
        for (int i = 0; i < BehaviourTreeManager.Granaries.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Granaries[i].gameObject;
            building.pos.x = BehaviourTreeManager.Granaries[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Granaries[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Granaries[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Granaries[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Granaries[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Granaries[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        #region House 
        for (int i = 0; i < BehaviourTreeManager.Houses.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Houses[i].gameObject;
            building.pos.x = BehaviourTreeManager.Houses[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Houses[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Houses[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Houses[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Houses[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Houses[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        #region StockPile
        for (int i = 0; i < BehaviourTreeManager.Stockpiles.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Stockpiles[i].gameObject;
            building.pos.x = BehaviourTreeManager.Stockpiles[i].transform.position.x;
            building.pos.y = BehaviourTreeManager.Stockpiles[i].transform.position.y;
            building.pos.z = BehaviourTreeManager.Stockpiles[i].transform.position.z;
            building.rot.x = BehaviourTreeManager.Stockpiles[i].transform.rotation.eulerAngles.x;
            building.rot.y = BehaviourTreeManager.Stockpiles[i].transform.rotation.eulerAngles.y;
            building.rot.z = BehaviourTreeManager.Stockpiles[i].transform.rotation.eulerAngles.z;
            buildings.Add(building);
        }
        #endregion

        return buildings;
    }

    #endregion

    #region Load
    public void Load()
    {
        //GameObject f = Instantiate(PrefabUtility.GetPrefabParent(BehaviourTreeManager.Colonists[0].gameObject), new Vector3(0, 5, 0), Quaternion.identity) as GameObject;
        //f.GetComponent<ColonistController>().colonistBaseMoveSpeed = 500;
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Open(path + "SaveData.dat", FileMode.Open);

        Dictionary<string, object> saveData = (Dictionary<string, object>)bF.Deserialize(file);


        #region Monsters
        List<DictionaryEntries> numHunters = new List<DictionaryEntries>();
        numHunters = (List<DictionaryEntries>)saveData["NumberOfHunters"];
        SetNumHunters(numHunters);


        List<MonsterData> monsterData = new List<MonsterData>();
        monsterData = (List<MonsterData>)saveData["Monsters"];
        LoadMonsters(monsterData);

        List<ColonistData> colonistData = new List<ColonistData>();
        //colonistData = (List<ColonistData>)saveData["Colonists"];
        //LoadColonists(colonistData);
        #endregion

        file.Close();
    }
    void SetNumHunters(List<DictionaryEntries> numHunters)
    {
        if (numHunters.Count > 0)
            for (int i = 0; i < numHunters.Count; i++)
            {
                string key = numHunters[i].key;
                MonsterTypes.Instance.Mons[key].numHuntersRequired = numHunters[i].value;
            }
    }
    void LoadMonsters(List<MonsterData> monsterData)
    {
        for (int i = 0; i < monsterData.Count; i++)
        {
            MonsterData currentMon = monsterData[i];
            Vector3 newPos = new Vector3(currentMon.Pos.x, currentMon.Pos.y, currentMon.Pos.z);
            MonsterController currentController = MonsterSpawner.Instance.SpawnMonster(newPos, monsterData[i].monsterType);
            currentController.transform.rotation.eulerAngles.Set(currentMon.Rot.x, currentMon.Rot.y, currentMon.Rot.z);
            currentController.beingHunted = currentMon.beingHunted;
            currentController.health = currentMon.health;
            currentController.hunger = currentMon.hunger;
            currentController.lastMatingTime = currentMon.lastMatingTime;
        }

    }
    void LoadColonists(List<ColonistData> colonistData)
    {
        for (int i = 0; i < colonistData.Count; i++)
        {
            ColonistData currentColonist = colonistData[i];

            Vector3 pos = new Vector3(currentColonist.Pos.x,currentColonist.Pos.y, currentColonist.Pos.z);
            ColonistJobType type = currentColonist.colonistJob;
            ColonistController thisController = ColonistSpawner.Instance.SpawnColonist(pos,type);
            thisController.transform.rotation.eulerAngles.Set(currentColonist.Rot.x, currentColonist.Rot.y, currentColonist.Rot.z);
            thisController.colonistName = currentColonist.colonistName;
            thisController.health = currentColonist.health;

            GameTime lastWorked = new GameTime();
            lastWorked.Date.x = currentColonist.lastWorked.Date.x;
            lastWorked.Date.y = currentColonist.lastWorked.Date.y;
            lastWorked.Date.z = currentColonist.lastWorked.Date.z;

            lastWorked.hours = (int)currentColonist.lastWorked.hours.x;
            lastWorked.minutes = (int)currentColonist.lastWorked.hours.y;
            thisController.lastWorked =lastWorked;

            thisController.colonistEquipment.EquipWearable(ItemDatabase.GetItem(currentColonist.legs) as Wearable);
            thisController.colonistEquipment.EquipWearable(ItemDatabase.GetItem(currentColonist.torso) as Wearable);
            thisController.colonistEquipment.EquipWearable(ItemDatabase.GetItem(currentColonist.weapon) as Wearable);




        }
        
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    #endregion
    #region Structs

    [System.Serializable]
    public struct MonsterData
    {
        public string monsterType;
        public MyVec3 Pos;
        public MyVec3 Rot;
        public float health;
        public float lastMatingTime;
        public float hunger;
        public bool beingHunted;
    }
    [System.Serializable]
    public struct ColonistData
    {
        public MyVec3 Pos;
        public MyVec3 Rot;
        public string colonistName;
        public float health;
        public MyGameTime timeOfNextMeal;
        public ColonistJobType colonistJob;
        public MyJob currentJob;
        public MyGameTime lastWorked;
        public string torso;
        public string legs;
        public string weapon;
    }
    [System.Serializable]
    public struct TerrainData
    {
        public MyVec3 pos;
        public MyVec3 rot;
        public GameObject gameObject;
        public string name;
    }
    [System.Serializable]
    public struct Building
    {
        public MyVec3 pos;
        public MyVec3 rot;
        public GameObject building;
        public string name;
    }
    [System.Serializable]
    public struct InventorySpace
    {
        public int currentRescourceAmount;
        public int rescourceSpace;
        public int nutritionSpace;

        public int currentWearableInventory;
        public int armourySpace;
    }
    [System.Serializable]
    public struct DictionaryEntries
    {
        public string key;
        public int value;
    }

    [System.Serializable]
    public struct MyVec3
    {
        public float x;
        public float y;
        public float z;
    }

    [System.Serializable]
    public struct MyGameTime
    {
        public MyVec3 hours;
        public MyVec3 Date;
    }
    public struct MyRequiredItem
    {
        public string resourceName;
        public int requiredAmount;
    }
    public struct MyJob
    {
        public string jobName;
        public float currentWorkAmount;
        public string jobType;
        public MyVec3 jobLocation;
        public string interactionObjectName;
        public string interactionItemName;
        public MyRequiredItem[] requiredItems;
    }
    #endregion
}
