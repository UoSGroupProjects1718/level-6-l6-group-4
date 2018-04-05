
using UnityEngine;

namespace MonsterMiner
{
    namespace BehaviourTree
    {
        [CreateAssetMenu(menuName = "Scriptable Objects/BehaviourTree/Leaf Nodes/Colonist/Hunter/Attack monster")]
        public class AttackMonster : BehaviourBase
        {
            public override Status UpdateFunc(ColonistController Colonist)
            {
                if (Colonist.target == null)
                    return Status.FAILURE;

                Colonist.transform.LookAt(Colonist.target.transform);

                if(Time.time > Colonist.nextAttack)
                {
                    if(HasHit(Colonist))
                    {
                        
                        Colonist.nextAttack = Time.time + Colonist.colonistEquipment.weapon.AttackSpeed;
                        Colonist.target.TakeDamage(Colonist.colonistEquipment.weapon.Damage,false);
                        if (Colonist.target.CheckDead())
                        {                             
                            Colonist.target = null;
                            Colonist.currentJob = null;
                            //update colonist UI
                            if (UIController.Instance.focusedColonist == Colonist)
                            {
                                UIController.Instance.UpdateColonistInfoPanel(Colonist);
                            }
                        }
                    }
                   
                }
                return Status.FAILURE;
            }
       
            private bool HasHit(ColonistController Colonist)
            {
                float hitChance = Random.Range(0, 100);
                return (hitChance <= Colonist.colonistEquipment.weapon.Accuracy);

            }
        }
    }
}
