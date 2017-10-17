using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Monsters/Make a Monster")]//Like colour a dinosaur but cooler
public class MonsterType : ScriptableObject {

    public string monsterName;
    public MonsterTypes.TypeOfMonster monsterType;
    
    public float monsterSpeed;
    public float health;
    public float maxHealth;
    public float combatRange;
    public float viewRange;
    public float damage;
    public float attackSpeed;
    public float nextAttack;
    public Mesh monsterMesh;
    public DropTable dropTable;
}
