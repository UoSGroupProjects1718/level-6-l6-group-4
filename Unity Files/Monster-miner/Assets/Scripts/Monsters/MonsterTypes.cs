using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTypes : SingletonClass<MonsterController> {
    public enum MonsterType
    {
        TypeOne,
        TypeTwo
    }
    public const int numberOfMonsters=0;

    float[] Health = new float[numberOfMonsters];

    Mesh[] MonsterMesh = new Mesh[numberOfMonsters];

    float[] Damage = new float[numberOfMonsters];

    
    public float getDamage(MonsterType type) {
        return Damage[getType(type)];
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
