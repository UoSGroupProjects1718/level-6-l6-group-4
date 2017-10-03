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

    public List<ColonistController> Colonists = new List<ColonistController>();
    public List<MonsterController> Monsters = new List<MonsterController>();

    public void Awake()
    {
     
    }
    public void FixedUpdate()
    {
        for(int i = 0; i < Colonists.Count; i++)
        {
            ColonistTree.UpdateFunc(Colonists[i]);
        }

        //uncomment when monsters have a tree
        //for (int i = 0; i < Monsters.Count; i++)
        //{
        //    MonsterTree.UpdateFunc(Monsters[i]);
        //}
    }


}
