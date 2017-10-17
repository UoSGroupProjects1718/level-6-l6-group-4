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
    
    MonsterType[] Monsters = new MonsterType[0];

    

    public void getMonsterData(string Type,
        out float returnHealth, out float returnSpeed, out float returnDamage, out float returnCombatRange, out float returnAttackSpeed ,out Mesh returnMesh,
        out DropTable returnDropTable
        ) {
        int type = getType(Type);
        returnHealth = Monsters[type].health;
        returnSpeed = Monsters[type].monsterSpeed;
        returnDamage = Monsters[type].damage;
        returnCombatRange = Monsters[type].combatRange;
        returnAttackSpeed = Monsters[type].attackSpeed;
        returnMesh = Monsters[type].monsterMesh;
        returnDropTable = Monsters[type].dropTable;
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
