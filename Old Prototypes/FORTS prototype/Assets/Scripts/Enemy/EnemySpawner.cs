using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject EnemyPrefab;
    public GameObject EnemyTransformParent;
    private GameObject[] EnemyPool;
    public int EnemyPoolSize = 60;
    private int enemyPoolIndex = 0;

    private GameObject[] SpawnPoints;
    private GameObject[] Destinations;

	// Use this for initialization
	void Start () {
        EnemyPool = new GameObject[EnemyPoolSize];
        for (int i = 0; i < EnemyPoolSize; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, EnemyTransformParent.transform) as GameObject;
            enemy.SetActive(false);
            EnemyPool[i] = enemy;
        }

        SpawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        Destinations = GameObject.FindGameObjectsWithTag("Destination");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Q))
        {
            print("Spawning enemies");
            SpawnEnemyWave(5);
        }
	}
    private void SpawnEnemyWave(int WaveSize)
    {
        StartCoroutine(spawn(WaveSize));
       
    }
    private Transform FindClosestDestination(Transform startPosition)
    {
        int CurrentClosest = 0;
        float currentMinLen = float.MaxValue;
        for (int i = 0; i < Destinations.Length; i++)
        {
            if(currentMinLen > (Destinations[i].transform.position - startPosition.position).magnitude)
            {
                currentMinLen = (Destinations[i].transform.position - startPosition.position).magnitude;
                CurrentClosest = i;
            }

        }
        return Destinations[CurrentClosest].transform;
    }
    public GameObject FindEnemy()
    {
        //loop through the pool
        for (int i = enemyPoolIndex; i < EnemyPoolSize; i++)
        {
            //if we find one which is inactive, we have found our object
            if (EnemyPool[i].activeSelf == false)
            {
                //if the index is at the end of the pool, reset it
                if (enemyPoolIndex + 1 == EnemyPoolSize)
                {
                    enemyPoolIndex = 0;
                }
                //otherwise save it for next time
                else
                {
                    enemyPoolIndex = i + 1;
                }
                //then return it
                return EnemyPool[i];
            }
        }
        //if we cant find anything, just set it to null
        return null;
    }
    private IEnumerator spawn(int WaveSize)
    {
        for (int i = 0; i < WaveSize; i++)
        {
            int SpawnPoint = Random.Range(0, SpawnPoints.Length);
            GameObject enemy = FindEnemy();
            enemy.transform.position = SpawnPoints[SpawnPoint].transform.position + Vector3.up;
            enemy.GetComponent<Unit>().target = FindClosestDestination(enemy.transform);
            enemy.SetActive(true);
            yield return new WaitForFixedUpdate();
        }
    }

}
