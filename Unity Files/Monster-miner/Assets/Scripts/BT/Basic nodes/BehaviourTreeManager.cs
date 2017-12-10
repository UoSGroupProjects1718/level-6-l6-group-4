using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;

public class BehaviourTreeManager : MonoBehaviour
{
    [SerializeField]
    private BehaviourBase ColonistTree;
    [SerializeField]
    private BehaviourBase MonsterTree;

    public static List<ColonistController> Colonists = new List<ColonistController>();
    public static List<MonsterController> Monsters = new List<MonsterController>();


    //building lists for finding the closest one to move to
    public static List<GranaryFunction> Granaries = new List<GranaryFunction>();
    public static List<StockpileFunction> Stockpiles = new List<StockpileFunction>();
    public static List<ArmouryFunction> Armouries = new List<ArmouryFunction>();
    public static List<BlacksmithFunction> Blacksmiths = new List<BlacksmithFunction>();





    private void Start()
    {



        for(int i = 0; i < 15; i++)
        {
            
            MonsterSpawner.Instance.SpawnMonster(Utils.RandomNavSphere(Vector3.zero,90,-1), MonsterTypes.Instance.dictionaryKeys[Random.Range(0,24)]);
        }

        StartCoroutine(BehaviourTrees());
    }


    IEnumerator BehaviourTrees()
    {
        while (true)
        {
            for (int i = 0; i < Colonists.Count; i++)
            {
                if (Colonists[i].isActiveAndEnabled)
                    ColonistTree.UpdateFunc(Colonists[i]);
            }

            for (int i = 0; i < Monsters.Count; i++)
            {
                if (Monsters[i].isActiveAndEnabled)
                    MonsterTree.UpdateFunc(Monsters[i]);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
