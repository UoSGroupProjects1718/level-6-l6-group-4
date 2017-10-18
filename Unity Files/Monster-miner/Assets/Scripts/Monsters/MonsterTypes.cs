using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MonsterTypes : SingletonClass<MonsterController> {

    public enum TypeOfMonster
    {
        SmallCarnivore,
        SmallHerbivore,
        LargeCarnivore,
        LargeHerbivore
    }
    
    public MonsterType[] Monsters = new MonsterType[0];
    Dictionary<string, MonsterType> Mons;


    private void Start()
    {
        Mons = new Dictionary<string, MonsterType>();
        for (int i = 0; i < Monsters.Length; i++)
        {
            Mons.Add(Monsters[i].monsterName, Monsters[i]);
        }
        Monsters = null;

       
    }
    public void getMonsterData(string Type,
        out float returnHealth, out float returnSpeed, out float returnDamage, out float returnCombatRange, out float returnAttackSpeed ,out Mesh returnMesh,
        out DropTable returnDropTable
        ) {
        returnHealth = Mons[Type].health;
        returnSpeed = Mons[Type].monsterSpeed;
        returnDamage = Mons[Type].damage;
        returnCombatRange = Mons[Type].combatRange;
        returnAttackSpeed = Mons[Type].attackSpeed;
        returnMesh = Mons[Type].monsterMesh;
        returnDropTable = Mons[Type].dropTable;
        return;
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
