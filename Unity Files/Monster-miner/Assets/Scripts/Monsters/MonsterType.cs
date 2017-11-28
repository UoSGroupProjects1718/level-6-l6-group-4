using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Monsters/Make a Monster")]//Like colour a dinosaur but cooler
public class MonsterType : ScriptableObject {

    public string monsterName;
    public MonsterTypes.TypeOfMonster monsterType;

    [HideInInspector]
    public int numHuntersRequired;
    public float monsterSpeed;
    public float maxHealth;
    public float combatRange;
    public float viewRange;
    public float damage;
    public float attackSpeed;
    public float maxHunger;
    [Range(0,100)]
    public float attackHungerPercentage;
    public float hungerLossPerSecond;
    public float hungerDamage;
    public float naturalRegeneration;
    [HideInInspector]
    public float nextAttack;
    public float matingCooldown;
    public GameObject monsterMeshAndBones;
    public DropTable dropTable;
    public Material[] materials;
    
}
