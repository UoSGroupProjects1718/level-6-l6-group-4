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
    float smallCarnivourMinMonsterSpawnRange = 100;
    [SerializeField]
    float smallCarnivourMaxMonsterSpawnRange = 400;
    [SerializeField]
    float smallHerbivourMinMonsterSpawnRange = 10;
    [SerializeField]
    float smallHerbivourMaxMonsterSpawnRange = 100;
    [SerializeField]
    float largeCarnivourMinMonsterSpawnRange = 400;
    [SerializeField]
    float largeCarnivourMaxMonsterSpawnRange = 600;
    [SerializeField]
    float largeHerbivourMinMonsterSpawnRange = 600;
    [SerializeField]
    float largeHerbivourMaxMonsterSpawnRange = 800;
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

        List<string> meatMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            meatMonsters.Add(usableKeys[i]);
        }

        

        List<string> boneMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            boneMonsters.Add(usableKeys[i+4]);
        }

        

        List<string> crystalMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            crystalMonsters.Add(usableKeys[i+8]);
        }
        

        List<string> ironMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            ironMonsters.Add(usableKeys[i+12]);
        }
        

        List<string> stoneMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            stoneMonsters.Add(usableKeys[i+16]);
        }
        

        List<string> woodMonsters = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            woodMonsters.Add(usableKeys[i+20]);
        }
        

        int monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(meatMonsters[monSpawn]);
        usableKeys.Remove(meatMonsters[monSpawn]);

        monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(boneMonsters[monSpawn]);
        usableKeys.Remove(boneMonsters[monSpawn]);

        monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(crystalMonsters[monSpawn]);
        usableKeys.Remove(crystalMonsters[monSpawn]);

        monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(ironMonsters[monSpawn]);
        usableKeys.Remove(ironMonsters[monSpawn]);

        monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(stoneMonsters[monSpawn]);
        usableKeys.Remove(stoneMonsters[monSpawn]);

        monSpawn = Random.Range(0, 4);
        SpawnMonsterGroup(woodMonsters[monSpawn]);
        usableKeys.Remove(woodMonsters[monSpawn]);

        monstersToSpawn -= 6;
        if (monstersToSpawn < 1) {
            return;
        }

        for (int i = 0; i < monstersToSpawn; i++)
        {
            int keyInt =Random.Range(0, usableKeys.Count - 1);
            
            string Key = usableKeys[keyInt];
            usableKeys.Remove(Key);
            SpawnMonsterGroup(Key);
        }
    }


    void SpawnMonsterGroup(string key)
    {

        float Range=0;
        switch (MonsterTypes.Instance.Mons[key].monsterType)
        {
            case MonsterTypes.TypeOfMonster.SmallCarnivore:
                Range= Random.Range(smallCarnivourMinMonsterSpawnRange, smallCarnivourMaxMonsterSpawnRange);
                break;
            case MonsterTypes.TypeOfMonster.SmallHerbivore:
                Range = Random.Range(smallHerbivourMinMonsterSpawnRange, smallHerbivourMaxMonsterSpawnRange);
                break;
            case MonsterTypes.TypeOfMonster.LargeCarnivore:
                Range = Random.Range(largeCarnivourMinMonsterSpawnRange, largeCarnivourMaxMonsterSpawnRange);
                break;
            case MonsterTypes.TypeOfMonster.LargeHerbivore:
                Range = Random.Range(largeHerbivourMinMonsterSpawnRange, largeHerbivourMaxMonsterSpawnRange);
                break;
            default:
                Range = 500;
                Debug.LogError("Dino unknown");
                return;
        }
        float Angle = Random.Range(0f, 2 * Mathf.PI);
        Vector2 Pos = new Vector2(Range * Mathf.Sin(Angle), Range * Mathf.Cos(Angle));
        int monstersPerGroup = Random.Range(minMonstersPerGroup, maxMonstersPerGroup);
        for (int j = 0; j < monstersPerGroup; j++)
        {
            Vector2 Offset = new Vector2(
                Random.Range(minMonsterXOffset, maxMonsterXOffset),
                Random.Range(minMonsterYOffset, maxMonsterYOffset)
            );
            Vector3 Spawnpos = new Vector3(Pos.x + Offset.x, 1, Pos.y + Offset.y);
            MonsterSpawner.Instance.SpawnMonster(Spawnpos, key);
        }
    }
}
