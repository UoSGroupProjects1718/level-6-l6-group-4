using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterTypes : SingletonClass<MonsterTypes> {

    public enum TypeOfMonster
    {
        SmallCarnivore,
        SmallHerbivore,
        LargeCarnivore,
        LargeHerbivore
    }
    
    public MonsterType[] Monsters = new MonsterType[0];
    public Dictionary<string, MonsterType> Mons;
    public List<string> dictionaryKeys;


    public override void Awake()
    {
        base.Awake();
        Mons = new Dictionary<string, MonsterType>();
        dictionaryKeys = new List<string>();
        for (int i = 0; i < Monsters.Length; i++)
        {
            //ONLY FOR EASE OF DEBUGGING//
            Monsters[i].numHuntersRequired = 1;
            /////////////////////////////////////////
            Mons.Add(Monsters[i].name, Monsters[i]);
            dictionaryKeys.Add(Monsters[i].name);

        }
        Monsters = null;

       
    }
    
    public void getMonsterData(string dictonaryKey,
        out float returnHealth, out float returnSpeed, out float returnDamage, out float returnCombatRange, 
        out float returnAttackSpeed,
        out DropTable returnDropTable, out float returnMatingCooldown, out int numHunters, out float viewRange
        ) {
        returnHealth = Mons[dictonaryKey].maxHealth;
        returnSpeed = Mons[dictonaryKey].monsterSpeed;
        returnDamage = Mons[dictonaryKey].damage;
        returnCombatRange = Mons[dictonaryKey].combatRange;
        returnAttackSpeed = Mons[dictonaryKey].attackSpeed;
        returnDropTable = Mons[dictonaryKey].dropTable;
        returnMatingCooldown = Mons[dictonaryKey].matingCooldown;
        numHunters = Mons[dictonaryKey].numHuntersRequired;
        viewRange = Mons[dictonaryKey].viewRange;
        return;
        
    }
    public void getNumHunters(MonsterController controller, out int numHunters)
    {
        numHunters = controller.numHunters;
    }
    int getType(string Name) {
        for (int i = 0; i < Monsters.Length; i++)
        {
            if (Monsters[i].monsterName == Name)
            {
                return i;
            }
        }
        Debug.Log("This wasnt meant to happen. Monster does not exist");
        return 0;
    }
	
}
