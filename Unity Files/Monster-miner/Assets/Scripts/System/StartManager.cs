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
        ColonistSpawner.Instance.SpawnColonist(new Vector3(100, 1, -100), ColonistJobType.Crafter);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-4, 1, 0), ColonistJobType.Scout);
        ColonistSpawner.Instance.SpawnColonist(new Vector3(-2, 1, -2), ColonistJobType.Hunter);
    }

    void SpawnMonsters()
    {
        int MinRange = 200;
        int MaxRange = 800;
        List<string> usableKeys = new List<string>();
        foreach (string key in MonsterTypes.Instance.dictionaryKeys)
        {
            usableKeys.Add(key);
        }
        int monstersToSpawn = Random.Range(4, 10);

        for (int i = 0; i < monstersToSpawn; i++)
        {
            int keyInt =Random.Range(0, usableKeys.Count - 1);
            float Range = Random.Range((float)MinRange, (float)MaxRange);
            float Angle = Random.Range(0f, 2*Mathf.PI);
            Vector2 Pos = new Vector2(Range * Mathf.Sin(Angle), Range * Mathf.Cos(Angle));
            string Key = usableKeys[keyInt];
            usableKeys.Remove(Key);
            for(int j=0; j < 4; j++) {
                Vector2 Offset = new Vector2(
                    Random.Range(-5f, 5f),
                    Random.Range(-5f, 5f)
                );
                Vector3 Spawnpos = new Vector3(Pos.x + Offset.x, 0, Pos.y + Offset.y);
                MonsterSpawner.Instance.SpawnMonster(Spawnpos, Key);
            }           
        }
    }
}
