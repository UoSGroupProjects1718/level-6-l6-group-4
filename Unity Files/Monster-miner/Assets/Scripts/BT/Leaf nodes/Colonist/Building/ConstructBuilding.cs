using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Construct Building")]
        public class ConstructBuilding : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob == null)
                    return Status.FAILURE;

                Colonist.currentJob.interactionObject.GetComponent<BuildingFunction>().Built = true;
                Colonist.currentJob.interactionObject.GetComponent<BuildingModelSwap>().UpdateObject();
                Colonist.currentJob.interactionObject.GetComponent<BuildingFunction>().OnBuilt();
                UIController.Instance.UpdateStockpile();
                //uncomment when model swapping for all buildings is correctly implemented + the scaffolding scale is correct
                //  Colonist.currentJob.InteractionObject.GetComponent<BuildingModelSwap>().UpdateObject();
                Debug.Log("Building Completed: " + Colonist.currentJob.interactionObject.name);
                switch(Colonist.currentJob.jobName)
                {
                    case "Build Granary":
                        BehaviourTreeManager.Granaries.Add(Colonist.currentJob.interactionObject.GetComponent<GranaryFunction>());
                        break;
                    case "Build Stockpile":
                        BehaviourTreeManager.Stockpiles.Add(Colonist.currentJob.interactionObject.GetComponent<StockpileFunction>());
                        break;
                    case "Build Armoury":
                        BehaviourTreeManager.Armouries.Add(Colonist.currentJob.interactionObject.GetComponent<ArmouryFunction>());
                        break;
                    case "Build Blacksmith":
                        BehaviourTreeManager.Blacksmiths.Add(Colonist.currentJob.interactionObject.GetComponent<BlacksmithFunction>());
                        break;
                    default:
                        break;
                }                
                Colonist.currentJob = null;
                return Status.SUCCESS;
            }
        }
    }
}
