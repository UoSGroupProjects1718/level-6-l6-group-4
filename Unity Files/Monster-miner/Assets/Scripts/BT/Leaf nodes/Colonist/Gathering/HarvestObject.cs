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
                //if the colonists job is null, the jobs interaction object is null or the drop table is not present, we have failed
                if (Colonist.currentJob == null || Colonist.currentJob.interactionObject != null && Colonist.currentJob.interactionObject.GetComponent<MonsterController>().dropTable == null)
                    return Status.FAILURE;

                SpawnDrops(Colonist.currentJob,Colonist.currentJob.interactionObject.GetComponent<MonsterController>().dropTable);
                Debug.Log("Finished harvesting" + Colonist.currentJob.interactionObject.GetComponent<MonsterController>().monsterName);
                //send the monster back to the pool
                for (int i = 0; i < Colonist.currentJob.interactionObject.transform.childCount; i++)
                {
                    Colonist.currentJob.interactionObject.transform.GetChild(i).gameObject.SetActive(false);
                }
                Colonist.currentJob.interactionObject.transform.DetachChildren();
                Colonist.currentJob = null;
                return Status.SUCCESS;
   
            }

            private void SpawnDrops(Job job,DropTable drops)
            {
                //loop through the drop table and create a new item for each item in the list
                for(int i = 0; i < drops.Drops.Length; i++)
                {
                    GameObject newItem = ItemDatabase.SpawnItemToWorld(drops.Drops[i].itemName);
                    Item item = newItem.GetComponent<Item>();
                    newItem.transform.localScale = new Vector3(1, 1, 1);
                    newItem.transform.position = job.jobLocation;
                    newItem.GetComponent<MeshRenderer>().material.color = Color.white;

                    //if it is not a wearable, set a random stack amount
                    if (item.item.type != ItemType.Wearable)
                    {
                        item.item.currentStackAmount = Random.Range((drops.Drops[i] as Resource).minDropAmount, (drops.Drops[i] as Resource).maxDropAmount);
                    }
                    //otherwise its a wearable and these dont stack
                    else
                        item.item.currentStackAmount = 1;


                    int WorkAmount = item.item.currentStackAmount * (item.item as Resource).GatherWorkPerItem;
                    JobManager.CreateJob(JobType.Gathering, WorkAmount, item.item,newItem,newItem.transform.position, "Gather " + item.item.itemName);
                    //the  items current job should be the one we have just added to the docket
                    item.correspondingJob = JobManager.Instance.JobDocket[JobManager.Instance.JobDocket.Count - 1];
                }
            }
            
        }
    }
}
