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


    public void FixedUpdate()
    {
        for(int i = 0; i < Colonists.Count; i++)
        {
            ColonistTree.UpdateFunc(Colonists[i]);
        }

        for (int i = 0; i < Monsters.Count; i++)
        {
            MonsterTree.UpdateFunc(Monsters[i]);
        }
    }


}