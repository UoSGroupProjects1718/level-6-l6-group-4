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
    public Dictionary<TypeOfMonster, MonsterType> Mons;


    public override void Awake()
    {
        base.Awake();
        Mons = new Dictionary<TypeOfMonster, MonsterType>();
        for (int i = 0; i < Monsters.Length; i++)
        {
            //ONLY FOR EASE OF DEBUGGING//
            Monsters[i].numHuntersRequired = 1;
            /////////////////////////////////////////
            Mons.Add(Monsters[i].monsterType, Monsters[i]);
        }
        Monsters = null;

       
    }
    public void getMonsterData(TypeOfMonster Type,
        out float returnHealth, out float returnSpeed, out float returnDamage, out float returnCombatRange, out float returnAttackSpeed, out Mesh returnMesh, out Material[] materials,
        out DropTable returnDropTable, out float returnMatingCooldown, out int numHunters, out float viewRange
        ) {
        returnHealth = Mons[Type].maxHealth;
        returnSpeed = Mons[Type].monsterSpeed;
        returnDamage = Mons[Type].damage;
        returnCombatRange = Mons[Type].combatRange;
        returnAttackSpeed = Mons[Type].attackSpeed;
        returnMesh = Mons[Type].monsterMesh;
        returnDropTable = Mons[Type].dropTable;
        returnMatingCooldown = Mons[Type].matingCooldown;
        numHunters = Mons[Type].numHuntersRequired;
        materials = Mons[Type].materials;
        viewRange = Mons[Type].viewRange;
        return;
    }
    public void getNumHunters(TypeOfMonster Type, out int numHunters)
    {
        numHunters = Mons[Type].numHuntersRequired;
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
