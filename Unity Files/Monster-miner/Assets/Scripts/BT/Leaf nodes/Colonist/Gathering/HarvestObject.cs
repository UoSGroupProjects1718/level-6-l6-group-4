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
                    SpawnDrops(Colonist.currentJob,Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().dropTable);
                    Debug.Log("Finished harvesting" + Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().monsterName);
                    Destroy(Colonist.currentJob.InteractionObject);
                    Colonist.currentJob = null;
                    return Status.SUCCESS;
                }
                Colonist.currentJob.currentWorkAmount -= Colonist.ColonistWorkSpeed * Time.deltaTime;
                return Status.RUNNING;
            }

            private void SpawnDrops(Job job,DropTable drops)
            {
                for(int i = 0; i < drops.Drops.Length; i++)
                {
                    GameObject newItem = Instantiate(ItemPrefab,job.InteractionObject.transform.position,Quaternion.identity);
                    Item Item = newItem.GetComponent<Item>();
                    newItem.transform.localScale = new Vector3(1, 1, 1);
                    newItem.GetComponent<MeshRenderer>().material.color = Color.white;
                    Item.item = Instantiate(drops.Drops[i]);
                    if (Item.item.type == ItemType.Nutrition || Item.item.type == ItemType.Resource)
                    {
                        Item.item.currentStackAmount = Random.Range((drops.Drops[i] as Resource).minDropAmount, (drops.Drops[i] as Resource).maxDropAmount);
                    }
                    else
                        Item.item.currentStackAmount = 1;
                    JobManager.Instance.CreateJob(JobType.Gathering, 20, newItem, newItem.transform.position, "Gather " + Item.item.name);
                    Item.UpdateMesh();
                    
                }
            }
            
        }
    }
}
