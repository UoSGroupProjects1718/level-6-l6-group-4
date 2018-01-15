using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

public class SaveData : MonoBehaviour {
    string path = "./";
	void Save()
    {
        BinaryFormatter bF = new BinaryFormatter();
        FileStream file = File.Create("/SaveData.dat");

        Dictionary<string, object> saveData = new Dictionary<string, object>();

        //save monsters
        
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

        //save colonists

        List<ColonistData> colonistData = new List<ColonistData>();

        for (int i = 0; i < BehaviourTreeManager.Colonists.Count; i++)
        {
            if (BehaviourTreeManager.Colonists[i].isActiveAndEnabled)
            {
                colonistData.Add(SaveColonistData(BehaviourTreeManager.Colonists[i]));
            }
        }
        saveData.Add("Colonists", colonistData);

        //Save Terrain
        GameObject terrainParent = GameObject.Find("Terrain");
        List<TerrainData> terrain = new List<TerrainData>();
        for (int i = 0; i < terrainParent.transform.childCount; i++)
        {
            TerrainData terrainData = new TerrainData();
            terrainData.gameObject = terrainParent.transform.GetChild(i).gameObject;
            terrainData.transform = terrainParent.transform.GetChild(i);
            terrain.Add(terrainData);
        }

        saveData.Add("Terrain", terrain);

        //Save buildings
        List<Building> buildings = SaveBuildings();

        saveData.Add("Buildings", buildings);


        saveData.Add("Time", TimeManager.Instance.IngameTime);



        KeyValuePair<ItemType,int>[] rescourceEntries = Stockpile.Instance.inventoryDictionary.ToArray();
        
        saveData.Add("RescourceEntries", rescourceEntries);


        KeyValuePair<Wearable, int>[] wearableRescourceEntries = Stockpile.Instance.wearableInventoryDictionary.ToArray();

        saveData.Add("WearableRescourceEntries", wearableRescourceEntries);


    }

    List<Building> SaveBuildings()
    {
        List<Building> buildings = new List<Building>();

        //armoury
        for(int i = 0; i < BehaviourTreeManager.Armouries.Count;i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Armouries[i].gameObject;
            building.transform = BehaviourTreeManager.Armouries[i].transform;
            buildings.Add(building);
        }

        //barracks
        for (int i = 0; i < BehaviourTreeManager.Barracks.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Barracks[i].gameObject;
            building.transform = BehaviourTreeManager.Barracks[i].transform;
            buildings.Add(building);
        }
        //blacksmith
        for (int i = 0; i < BehaviourTreeManager.Blacksmiths.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Blacksmiths[i].gameObject;
            building.transform = BehaviourTreeManager.Blacksmiths[i].transform;
            buildings.Add(building);
        }
        //granary
        for (int i = 0; i < BehaviourTreeManager.Granaries.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Granaries[i].gameObject;
            building.transform = BehaviourTreeManager.Granaries[i].transform;
            buildings.Add(building);
        }
        //house 
        for (int i = 0; i < BehaviourTreeManager.Houses.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Houses[i].gameObject;
            building.transform = BehaviourTreeManager.Houses[i].transform;
            buildings.Add(building);
        }
        //stockpile
        for (int i = 0; i < BehaviourTreeManager.Stockpiles.Count; i++)
        {
            Building building = new Building();
            building.building = BehaviourTreeManager.Stockpiles[i].gameObject;
            building.transform = BehaviourTreeManager.Stockpiles[i].transform;
            buildings.Add(building);
        }

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
        data.transform = monster.gameObject.transform;
        return data;
    }

    ColonistData SaveColonistData(ColonistController colonist)
    {
        ColonistData data = new ColonistData();
        data.torso = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Torso].itemName;
        data.legs = colonist.colonistEquipment.equippedArmour[(int)ArmourSlot.Legs].itemName;
        data.weapon = colonist.colonistEquipment.weapon.itemName;
        data.colonistJob = colonist.colonistJob;
        data.currentJob = SaveColonistJob(colonist.currentJob);
        data.colonistName = colonist.colonistName;
        data.health = colonist.health;
        data.lastWorked = colonist.lastWorked;
        data.timeOfNextMeal = colonist.timeOfNextMeal;
        return data;
    }

    Job SaveColonistJob(Job currentJob)
    {
        Job returnJob = new Job();
        returnJob.currentWorkAmount = currentJob.currentWorkAmount;
        returnJob.interactionItem = currentJob.interactionItem;
        returnJob.interactionObject = currentJob.interactionObject;
        returnJob.jobLocation = currentJob.jobLocation;
        returnJob.jobName = currentJob.jobName;
        returnJob.jobType = currentJob.jobType;
        returnJob.maxWorkAmount = currentJob.maxWorkAmount;
        returnJob.RequiredItems = currentJob.RequiredItems;
        return returnJob;
    }


    #region Structs


    public struct MonsterData
    {
        public Transform transform;
        public string monsterType;
        public float health;
        public float lastMatingTime;
        public float hunger;
        public bool beingHunted;
    }

    public struct ColonistData
    {
        public string colonistName;
        public float health;
        public GameTime timeOfNextMeal;
        public ColonistJobType colonistJob;
        public Job currentJob;
        public GameTime lastWorked;
        public string torso;
        public string legs;
        public string weapon;
    }

    public struct TerrainData
    {
        public Transform transform;
        public GameObject gameObject;
    }

    public struct Building
    {
        public Transform transform;
        public GameObject building;
    }

    public struct InventorySpace
    {
        public int currentRescourceAmount;
        public int rescourceSpace;
        public int nutritionSpace;

        public int currentWearableInventory;
        public int armourySpace;
    }

    public struct DictionaryEntries
    {
        public string key;
        public int value;
    }

    public struct MyStruct
    {
        ItemType type;
        int value;
    }





    #endregion
}
