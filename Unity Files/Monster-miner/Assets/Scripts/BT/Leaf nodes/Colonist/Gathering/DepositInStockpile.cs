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
                //if(Colonist.currentJob.InteractionObject.GetComponent<Item>().item.type == ItemType.Nutrition)
                //{
                
                if(Stockpile.Instance.AddResource(Colonist.currentJob.InteractionObject.GetComponent<Item>().item as Resource))
                {
                    Destroy(Colonist.currentJob.InteractionObject);
                    Colonist.currentJob = null;
                    Colonist.GathererStockpile = null;
                }
                else
                {
                    Colonist.currentJob.InteractionObject.transform.position = Colonist.transform.position;
                    Colonist.currentJob.InteractionObject.GetComponent<MeshRenderer>().enabled = true;
                    Colonist.currentJob = null;
                    Colonist.GathererStockpile = null;

                }
               // }
                //else
                //{
                //    Colonist.GathererStockpile.GetComponent<StockpileFunction>().AddItem(Colonist.currentJob.InteractionObject.GetComponent<Item>().item);
                //    Colonist.currentJob = null;
                //    Colonist.GathererStockpile = null;
                //}
                return Status.SUCCESS;
            }
        }
	}
}
