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


    public void NewWorldAwake()
    {
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

    public void LoadWorldAwake()
    {
        base.Awake();
        Mons = new Dictionary<string, MonsterType>();
        dictionaryKeys = new List<string>();
    }

    public void getMonsterData(string dictionaryKey,
        out float returnHealth, out float returnHunger, out float returnHungerAttackPercentage, out float returnHungerDamage, out float hungerLossPerSecond, out float returnNaturalRegen, out float returnSpeed, out float returnDamage, out float returnCombatRange, 
        out float returnAttackSpeed,
        out DropTable returnDropTable, out float returnMatingCooldown, out int numHunters, out float viewRange, out TypeOfMonster monsType
        ) {
        returnHealth = Mons[dictionaryKey].maxHealth;
        returnHunger = Mons[dictionaryKey].maxHunger;
        returnNaturalRegen = Mons[dictionaryKey].naturalRegeneration;
        returnHungerDamage = Mons[dictionaryKey].hungerDamage;
        returnHungerAttackPercentage = Mons[dictionaryKey].attackHungerPercentage;
        hungerLossPerSecond = Mons[dictionaryKey].hungerLossPerSecond;
        returnSpeed = Mons[dictionaryKey].monsterSpeed;
        returnDamage = Mons[dictionaryKey].damage;
        returnCombatRange = Mons[dictionaryKey].combatRange;
        returnAttackSpeed = Mons[dictionaryKey].attackSpeed;
        returnDropTable = Mons[dictionaryKey].dropTable;
        returnMatingCooldown = Mons[dictionaryKey].matingCooldown;
        numHunters = Mons[dictionaryKey].numHuntersRequired;
        viewRange = Mons[dictionaryKey].viewRange;
        monsType = Mons[dictionaryKey].monsterType;

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
