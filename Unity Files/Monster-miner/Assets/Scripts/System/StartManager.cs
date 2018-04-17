using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartManager : MonoBehaviour {
    public TerrainSpawner terrainSpawner;
    string path = "./";
    // Use this for initialization
    [Header("Monster spawning editor")]
    [SerializeField]
    float minMonsterSpawnRange = 200;
    [SerializeField]
    float maxMonsterSpawnRange = 800;
    [SerializeField]
    float minMonsterXOffset = -20f;
    [SerializeField]
    float maxMonsterXOffset = 20f;
    [SerializeField]
    float minMonsterYOffset = -20f;
    [SerializeField]
    float maxMonsterYOffset = 20f;
    [SerializeField]
    int minMonstersToSpawn = 4;
    [SerializeField]
    int maxMonstersToSpawn = 10;
    [SerializeField]
    int minMonstersPerGroup = 4;
    [SerializeField]
    int maxMonstersPerGroup = 4;

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
        SpawnColonists();
        SpawnMonsters();
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

    void SpawnColonists() {
        ColonistSpawner.Instance.SpawnColonist(new Vector3(1, 1, -1), ColonistJobType.Crafter);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-4, 1, 0), ColonistJobType.Scout);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-2, 1, -1), ColonistJobType.Hunter);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-5, 1, -2), ColonistJobType.Hunter);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-2, 1, -5), ColonistJobType.Hunter);
    }

    void SpawnMonsters()
    {
        
        List<string> usableKeys = new List<string>();
        foreach (string key in MonsterTypes.Instance.dictionaryKeys)
        {
            usableKeys.Add(key);
        }
        int monstersToSpawn = Random.Range(minMonstersToSpawn, maxMonstersToSpawn);
        
        for (int i = 0; i < monstersToSpawn; i++)
        {
            int keyInt =Random.Range(0, usableKeys.Count - 1);
            float Range = Random.Range(minMonsterSpawnRange, maxMonsterSpawnRange);
            float Angle = Random.Range(0f, 2*Mathf.PI);
            Vector2 Pos = new Vector2(Range * Mathf.Sin(Angle), Range * Mathf.Cos(Angle));
            string Key = usableKeys[keyInt];
            usableKeys.Remove(Key);
            int monstersPerGroup = Random.Range(minMonstersPerGroup, maxMonstersPerGroup);
            for (int j=0; j < monstersPerGroup; j++) {
                Vector2 Offset = new Vector2(
                    Random.Range(minMonsterXOffset, maxMonsterXOffset),
                    Random.Range(minMonsterYOffset, maxMonsterYOffset)
                );
                Vector3 Spawnpos = new Vector3(Pos.x + Offset.x, 1, Pos.y + Offset.y);
                MonsterSpawner.Instance.SpawnMonster(Spawnpos, Key);
            }           
        }
    }
}
