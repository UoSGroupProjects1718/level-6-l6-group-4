using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StartManager : MonoBehaviour {
    public MonsterTypes monsterTypes;
    public TerrainSpawner terrainSpawner;
    string path = "";
    // Use this for initialization
    void Start () {
        
        TimeManager.Instance.BeginTime();
        
        if (FileExist())
        {
            LoadWorld();
        }

        else
        {
            GenerateWorld();
        }
        MonsterSpawner.Instance.SpawnMonsterLists();
        ColonistSpawner.Instance.SpawnColonistLists();

    }

    void GenerateWorld() {
        StartCoroutine(terrainSpawner.SpawnNewWorld());
        monsterTypes.NewWorldAwake();
        
    }


    void LoadWorld() {
        StartCoroutine(terrainSpawner.LoadWorld());
        monsterTypes.LoadWorldAwake();
    }

	
	public bool FileExist()
    {
        return (File.Exists(path + "/SaveData.dat")) ;
    }
}
