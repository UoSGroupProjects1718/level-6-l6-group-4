using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ Deposit in stockpile")]
        public class DepositInStockpile : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if(Colonist.currentJob.InteractionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
                {
                
                   Colonist.GathererStockpile.GetComponent<GranaryFunction>().AddItem(Colonist.currentJob.InteractionObject.GetComponent<Item>().item);
                    Colonist.currentJob = null;
                    Colonist.GathererStockpile = null;
                }
                else
                {
                    Colonist.GathererStockpile.GetComponent<StockpileFunction>().AddItem(Colonist.currentJob.InteractionObject.GetComponent<Item>().item);
                    Colonist.currentJob = null;
                    Colonist.GathererStockpile = null;
                }
                return Status.SUCCESS;
            }
        }
	}
}
