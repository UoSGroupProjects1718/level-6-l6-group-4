using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterMiner.BehaviourTree;

public class BehaviourTreeManager : SingletonClass<BehaviourTreeManager>
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
    public static List<HouseFunction> Houses = new List<HouseFunction>();
    public static List<BarracksFunction> Barracks = new List<BarracksFunction>();








    public IEnumerator BehaviourTrees()
    {
        while (true)
        {
            try
            {
                for (int i = 0; i < Colonists.Count; i++)
                {
                    if (Colonists[i].isActiveAndEnabled)
                        ColonistTree.UpdateFunc(Colonists[i]);
                }
            }
            catch
            {
                Debug.LogError("Colonist tree errored out");
            }
            try
            {
                for (int i = 0; i < Monsters.Count; i++)
                {
                    if (Monsters[i].isActiveAndEnabled)
                        MonsterTree.UpdateFunc(Monsters[i]);
                }
            }
            catch
            {
                Debug.LogError("Monster tree errored out");
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
