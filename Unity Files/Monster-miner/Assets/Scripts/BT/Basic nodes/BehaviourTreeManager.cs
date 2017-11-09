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

    static public List<ColonistController> Colonists = new List<ColonistController>();
    static public List<MonsterController> Monsters = new List<MonsterController>();

    static public List<GranaryFunction> Granaries = new List<GranaryFunction>();
    static public List<StockpileFunction> Stockpiles = new List<StockpileFunction>();


    private void Start()
    {
        StartCoroutine(BehaviourTrees());
    }


    IEnumerator BehaviourTrees()
    {
        while (true)
        {
            for (int i = 0; i < Colonists.Count; i++)
            {
                if(Colonists[i].isActiveAndEnabled)
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
