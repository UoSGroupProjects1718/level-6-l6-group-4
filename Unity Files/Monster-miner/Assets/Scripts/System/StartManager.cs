using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartManager : MonoBehaviour {
    public MonsterTypes monsterTypes;
    public TerrainSpawner terrainSpawner;
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
        //check if the save file exists
        return false;
    }
}
