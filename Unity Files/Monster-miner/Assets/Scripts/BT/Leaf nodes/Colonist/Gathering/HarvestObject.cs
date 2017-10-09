using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/ Harvest Object")]

        public class HarvestObject : BehaviourBase
        {

            [SerializeField]
            private GameObject ItemPrefab;

            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.currentJob == null || Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().dropTable == null)
                    return Status.FAILURE;

                if(Colonist.currentJob.currentWorkAmount <= 0)
                {
                    SpawnDrops(Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().dropTable);
                    Debug.Log("Finished harvesting" + Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().MonsterName);
                    Destroy(Colonist.currentJob.InteractionObject);
                    return Status.SUCCESS;
                }
                Colonist.currentJob.currentWorkAmount -= Colonist.ColonistWorkSpeed * Time.deltaTime;
                return Status.RUNNING;
            }
            private void SpawnDrops(DropTable drops)
            {
                for(int i = 0; i < drops.Drops.Length; i++)
                {
                    GameObject newItem = Instantiate(ItemPrefab);
                    newItem.GetComponent<Item>().item = Instantiate(drops.Drops[i]);
                }
            }
        }
    }
}
