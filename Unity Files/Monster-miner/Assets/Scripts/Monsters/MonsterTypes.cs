using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTypes : SingletonClass<MonsterController> {
    public enum MonsterType
    {
        TypeOne,
        TypeTwo
    }

    [SerializeField]
    const int numberOfMonsters=0; 
    [SerializeField]
    public float[] Health = new float[numberOfMonsters];
    [SerializeField]
    public float[] Speed = new float[numberOfMonsters];
    [SerializeField]
    public float[] Damage = new float[numberOfMonsters];
    [SerializeField]
    public float[] CombatRange = new float[numberOfMonsters];
    [SerializeField]
    public float[] AttackSpeed = new float[numberOfMonsters];
    [SerializeField]
    public Mesh[] MonsterMesh = new Mesh[numberOfMonsters];
    [SerializeField]
    DropTable[] Drops = new DropTable[numberOfMonsters];

    

    public void getMonsterData(MonsterType Type,
        out float returnHealth, out float returnSpeed, out float returnDamage, out float returnCombatRange, out float returnAttackSpeed ,out Mesh returnMesh,
        out DropTable returnDropTable
        ) {
        int type = getType(Type);
        returnHealth = Health[type];
        returnSpeed = Speed[type];
        returnDamage = Damage[type];
        returnCombatRange = CombatRange[type];
        returnAttackSpeed = AttackSpeed[type];
        returnMesh = MonsterMesh[type];
        returnDropTable = Drops[type];
        return;
    }

    int getType(MonsterType type) {
        switch (type)
        {
            case MonsterType.TypeOne:
                return 0;
            case MonsterType.TypeTwo:
                return 1;
            default:
                return 0;
        }
    }
	
}
