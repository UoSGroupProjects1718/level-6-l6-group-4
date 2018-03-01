using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartManager : MonoBehaviour {
    public TerrainSpawner terrainSpawner;
    string path = "./";
    // Use this for initialization


    void Start(){
        /*This will delete the save*/
        File.Delete(path + "SaveData.dat");

        TimeManager.Instance.BeginTime();

        if (FileExist())
        {
            Debug.Log("Found Save");
            LoadWorld();
        }

        else
        {
            Debug.Log("No Save Found");
            GenerateWorld();
        }
        MonsterSpawner.Instance.SpawnMonsterLists();
        ColonistSpawner.Instance.SpawnColonistLists();

        StartCoroutine(BehaviourTreeManager.Instance.BehaviourTrees());
        ColonistSpawner.Instance.SpawnColonist(new Vector3(100, 1, -100), ColonistJobType.Crafter);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-4, 1, 0), ColonistJobType.Scout);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-2, 1, -2), ColonistJobType.Hunter);
        MonsterSpawner.Instance.SpawnMonster(new Vector3(-1, 0, 0), "Small Bone Herbivore");
        MonsterSpawner.Instance.SpawnMonster(new Vector3(-1, 0, 1), "Large Bone Herbivore");
        MonsterSpawner.Instance.SpawnMonster(new Vector3(1, 0, -1), "Small Bone Carnivore");
        MonsterSpawner.Instance.SpawnMonster(new Vector3(1, 0, 1), "Large Bone Carnivore");
    }

    void GenerateWorld() {
        terrainSpawner.SpawnNewWorld();
        //MonsterSpawner.Instance.NewWorldSpawnMonsters();
    }

    void LoadWorld() {
        StartCoroutine(terrainSpawner.LoadWorld());
    }

	public bool FileExist()
    {
        
        return (File.Exists(path + "SaveData.dat")) ;
    }
}
