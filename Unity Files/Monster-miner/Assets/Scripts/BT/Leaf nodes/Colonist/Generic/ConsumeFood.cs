using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "BehaviourTree/Leaf nodes/Colonist/Generic/ConsumeFood")]
        public class ConsumeFood : BehaviourBase
        { 

            public override Status UpdateFunc(ColonistController Colonist)
            {
                //if it is time to eat, check if we can eat the colonist's required nutrition amount and then remove it
                if(TimeManager.Instance.IngameTime.Date == Colonist.timeOfNextMeal.Date)
                {
                    if (TimeManager.Instance.IngameTime.hours == Colonist.timeOfNextMeal.hours)
                    {
                        if (Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] >= Colonist.requiredNutritionPerDay)
                        {
                            Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] -= Colonist.requiredNutritionPerDay;
                            Colonist.SetTimeOfNextMeal();
                            UIController.Instance.UpdateStockpile();
                            //heal colonists once per day
                            if(Colonist.health + Colonist.maxHealth/10 < Colonist.maxHealth)
                            {
                                Colonist.health += Colonist.maxHealth / 10;
                            }
                            if(Colonist.health + Colonist.maxHealth/10 >= Colonist.maxHealth)
                            {
                                Colonist.health = Colonist.maxHealth;
                            }
                            //update colonist info panel
                            if (UIController.Instance.focusedColonist == Colonist)
                            {
                                UIController.Instance.UpdateColonistInfoPanel(Colonist);
                            }

                            return Status.SUCCESS;

                        }
                        //if not, figure out the amount we can take out, eat that and then reduce colonist health by 1 for each nutrition they could not consume
                        else
                        {
   
                            Stockpile.Instance.inventoryDictionary[ItemType.Nutrition] =0;
                            Colonist.health -= Colonist.maxHealth / 10;
                            
                            Colonist.SetTimeOfNextMeal();
                            UIController.Instance.UpdateStockpile();
                            //update colonist info panel
                            if (UIController.Instance.focusedColonist == Colonist)
                            {
                                UIController.Instance.UpdateColonistInfoPanel(Colonist);
                            }
                            return Status.SUCCESS;
                        }
                    }
                }
                return Status.FAILURE;
            }

        }
        
    }
}

