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
                if (Colonist.currentJob == null || Colonist.currentJob.InteractionObject != null && Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().dropTable == null)
                    return Status.FAILURE;


                SpawnDrops(Colonist.currentJob,Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().dropTable);
                Debug.Log("Finished harvesting" + Colonist.currentJob.InteractionObject.GetComponent<MonsterController>().monsterName);
                //Destroy(Colonist.currentJob.InteractionObject);
                for (int i = 0; i < Colonist.currentJob.InteractionObject.transform.childCount; i++)
                {
                    Colonist.currentJob.InteractionObject.transform.GetChild(i).gameObject.SetActive(false);
                }
                Colonist.currentJob.InteractionObject.transform.DetachChildren();
                Colonist.currentJob = null;
                return Status.SUCCESS;
   
            }

            private void SpawnDrops(Job job,DropTable drops)
            {
                //loop through the drop table and create a new item for each item in the list
                for(int i = 0; i < drops.Drops.Length; i++)
                {
                    GameObject newItem = ItemDatabase.SpawnItemToWorld(drops.Drops[i].itemName);
                    Item Item = newItem.GetComponent<Item>();
                    newItem.transform.localScale = new Vector3(1, 1, 1);
                    newItem.transform.position = job.jobLocation;
                    newItem.GetComponent<MeshRenderer>().material.color = Color.white;

                    //if it is not a wearable, set a random stack amount
                    if (Item.item.type != ItemType.Wearable)
                    {
                        Item.item.currentStackAmount = Random.Range((drops.Drops[i] as Resource).minDropAmount, (drops.Drops[i] as Resource).maxDropAmount);
                    }
                    //otherwise its a wearable and these dont stack
                    else
                        Item.item.currentStackAmount = 1;


                    int WorkAmount = Item.item.currentStackAmount * (Item.item as Resource).GatherWorkPerItem;
                    JobManager.CreateJob(JobType.Gathering, WorkAmount, newItem, newItem.transform.position, "Gather " + Item.item.name);                    
                }
            }
            
        }
    }
}
