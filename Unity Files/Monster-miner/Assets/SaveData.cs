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
     
     */

public class SaveData : MonoBehaviour {
    string path = "./";
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
        saveData.Add("Colonists", colonistData);
        #endregion
        #region TerrainData
        GameObject terrainParent = GameObject.Find("Terrain");
        List<TerrainData> terrain = new List<TerrainData>();
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

        saveData.Add("Terrain", terrain);
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

    public void Load()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Open(path + "SaveData.dat",FileMode.Open);
        
        Dictionary<string, object> saveData = (Dictionary<string, object>)bF.Deserialize(file);        
    }

    List<Building> SaveBuildings()
    {
        List<Building> buildings = new List<Building>();
        #region Armouries
        //armoury
        for (int i = 0; i < BehaviourTreeManager.Armouries.Count;i++)
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

    MonsterData SaveMonsterData(MonsterController monster)
    {
        MonsterData data = new MonsterData();
        data.beingHunted = monster.beingHunted;
        data.health = monster.health;
        data.hunger = monster.hunger;
        data.lastMatingTime = monster.lastMatingTime;
        data.monsterType = monster.monsterType.ToString();
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
        data.Pos.x = colonist.transform.position.x;
        data.Pos.y = colonist.transform.position.y;
        data.Pos.z = colonist.transform.position.z;
        data.Rot.x = colonist.transform.rotation.eulerAngles.x;
        data.Rot.y = colonist.transform.rotation.eulerAngles.y;
        data.Rot.z = colonist.transform.rotation.eulerAngles.z;
        data.torso = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Torso].itemName;
        data.legs = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Legs].itemName;
        data.weapon = colonist.colonistEquipment.weapon.itemName;
        data.colonistJob = colonist.colonistJob;
        data.currentJob = SaveColonistJob(colonist.currentJob);
        data.colonistName = colonist.colonistName;
        data.health = colonist.health;
        data.lastWorked.hours.x = colonist.timeOfNextMeal.hours;
        data.lastWorked.hours.y = colonist.timeOfNextMeal.minutes;
        data.lastWorked.Date.x = colonist.timeOfNextMeal.Date.x;
        data.lastWorked.Date.y = colonist.timeOfNextMeal.Date.y;
        data.lastWorked.Date.z = colonist.timeOfNextMeal.Date.z;

        MyGameTime nextMeal = new MyGameTime();
        nextMeal.hours.x = colonist.timeOfNextMeal.hours;
        nextMeal.hours.y = colonist.timeOfNextMeal.minutes;
        nextMeal.Date.x = colonist.timeOfNextMeal.Date.x;
        nextMeal.Date.y = colonist.timeOfNextMeal.Date.y;
        nextMeal.Date.z = colonist.timeOfNextMeal.Date.z;

        data.timeOfNextMeal = nextMeal;
        return data;
    }

    Job SaveColonistJob(Job currentJob)
    {
        Job returnJob = new Job();
        try //ColonistJob may be null
        {
            returnJob.currentWorkAmount = currentJob.currentWorkAmount;
            returnJob.interactionItem = currentJob.interactionItem;
            returnJob.interactionObject = currentJob.interactionObject;
            returnJob.jobLocation = currentJob.jobLocation;
            returnJob.jobName = currentJob.jobName;
            returnJob.jobType = currentJob.jobType;
            returnJob.maxWorkAmount = currentJob.maxWorkAmount;
            returnJob.RequiredItems = currentJob.RequiredItems;
        }
        catch
        {}
        return returnJob;
    }

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
        public Job currentJob;
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
    #endregion
}
