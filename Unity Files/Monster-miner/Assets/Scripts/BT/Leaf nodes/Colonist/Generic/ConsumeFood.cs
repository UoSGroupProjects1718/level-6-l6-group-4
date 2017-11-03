using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/ BehaviourTree/Leaf nodes/Colonist/Generic/ConsumeFood")]
        public class ConsumeFood : BehaviourBase
        { 

            public override Status UpdateFunc(ColonistController Colonist)
            {
                //if it is time to eat, check if we can eat the colonist's required nutrition amount and then remove it
                if(TimeManager.Instance.IngameTime.Date == Colonist.timeOfNextMeal.Date)
                {
                    if (TimeManager.Instance.IngameTime.hours == Colonist.timeOfNextMeal.hours)
                    {
                        if (Stockpile.Instance.InventoryDictionary[ItemType.Nutrition] >= Colonist.requiredNutritionPerDay)
                        {
                            Stockpile.Instance.InventoryDictionary[ItemType.Nutrition] -= Colonist.requiredNutritionPerDay;
                            Colonist.SetTimeOfNextMeal();
                            UIController.Instance.UpdateStockpile();
                            return Status.SUCCESS;
                        }
                        //if not, figure out the amount we can take out, eat that and then reduce colonist health by 1 for each nutrition they could not consume
                        else
                        {
                            int AmountAvailable = Colonist.requiredNutritionPerDay - Stockpile.Instance.InventoryDictionary[ItemType.Nutrition];
                            Stockpile.Instance.InventoryDictionary[ItemType.Nutrition] -= AmountAvailable;
                            Colonist.health -= Colonist.maxHealth / 10;
                            Colonist.colonistWorkSpeed -= Colonist.colonistBaseWorkSpeed / 10;
                            Colonist.colonistMoveSpeed -= Colonist.colonistBaseMoveSpeed / 10;
                            Colonist.SetTimeOfNextMeal();
                            UIController.Instance.UpdateStockpile();
                            return Status.SUCCESS;
                        }
                    }
                }
                return Status.FAILURE;
            }

        }
        
    }
}

