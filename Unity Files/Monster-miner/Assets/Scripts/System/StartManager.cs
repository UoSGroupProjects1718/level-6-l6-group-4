using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartManager : MonoBehaviour {
    public MonsterTypes monsterTypes;
    public TerrainSpawner terrainSpawner;
    string path = "./";
    // Use this for initialization
    void Start () {
        /*This will delete the save*/
        File.Delete(path + "SaveData.dat");

        TimeManager.Instance.BeginTime();
        MonsterSpawner.Instance.SpawnMonsterLists();
        ColonistSpawner.Instance.SpawnColonistLists();
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

        StartCoroutine(BehaviourTreeManager.Instance.BehaviourTrees());

    }

    void GenerateWorld() {
        StartCoroutine(terrainSpawner.SpawnNewWorld());
        monsterTypes.NewWorldAwake();
        MonsterSpawner.Instance.NewWorldSpawnMonsters();
    }

    void LoadWorld() {
        StartCoroutine(terrainSpawner.LoadWorld());
        monsterTypes.LoadWorldAwake();
    }

	public bool FileExist()
    {
        return (File.Exists(path + "SaveData.dat")) ;
    }
}
