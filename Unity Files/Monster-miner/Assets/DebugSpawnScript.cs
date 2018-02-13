using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSpawnScript : MonoBehaviour {

    MonsterSpawner spawner;
    private void Start()
    {
        spawner = MonsterSpawner.Instance;
    }

    public void LargeBasicCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Carnivore");
    }

    public void LargeBasicHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Herbivore");
    }

    public void SmallBasicCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Carnivore");
    }

    public void SmallBasicHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Herbivore");
    }

    public void LargeBoneCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Bone Carnivore");
    }

    public void LargeBoneHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Bone Herbivore");
    }

    public void SmallBoneCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Bone Carnivore");
    }

    public void SmallBoneHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Bone Herbivore");
    }

    public void LargeCrystalCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Crystal Carnivore");
    }

    public void LargeCrystalHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Cystal Herbivore");
    }

    public void SmallCrystalCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Crystal Carnivore");
    }

    public void SmallCrystalHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Crystal Herbivore");
    }

    public void LargeIronCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Iron Carnivore");
    }

    public void LargeIronHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Iron Herbivore");
    }

    public void SmallIronCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Iron Carnivore");
    }

    public void SmallIronHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Iron Herbivore");
    }

    public void LargeStoneCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Stone Carnivore");
    }

    public void LargeStoneHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Stone Herbivore");
    }

    public void SmallStoneCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Stone Carnivore");
    }

    public void SmallStoneHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Stone Herbivore");
    }

    public void LargeWoodCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Wood Carnivore");
    }

    public void LargeWoodHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Large Wood Herbivore");
    }

    public void SmallWoodCarnivour()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Wood Carnivore");
    }

    public void SmallWoodHerbivore()
    {
        spawner.SpawnMonster(new Vector3(0, 0, 0), "Small Wood Herbivore");
    }
    public void CrafterColonist()
    {
        ColonistSpawner.Instance.SpawnColonist(new Vector3(0, 0, 0), ColonistJobType.Crafter);
    }

    public void HunterColonist()
    {
        ColonistSpawner.Instance.SpawnColonist(new Vector3(0, 0, 0), ColonistJobType.Hunter);
    }

    public void ScoutColonist()
    {
        ColonistSpawner.Instance.SpawnColonist(new Vector3(0, 0, 0), ColonistJobType.Scout);
    }
}
