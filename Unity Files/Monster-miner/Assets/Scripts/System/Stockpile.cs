using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile : SingletonClass<Stockpile>
{


    public ResourceTypeDictionary InventoryDictionary;
    [SerializeField]
    int CurrResourceAmount;
    [SerializeField]
    int ResourceSpace;
    [SerializeField]
    int NutritionSpace;
   

    public override void Awake()
    {
        base.Awake();

    }

    public bool AddResource(Resource res)
    {
        
        switch (res.type)
        {
            case ItemType.Wood:
                //if we can add the resource given the space we have, increase the amount of the resource and the current amount of resources
                if(CurrResourceAmount + res.currentStackAmount <= ResourceSpace)
                {
                    InventoryDictionary[ItemType.Wood] += res.currentStackAmount;
                    CurrResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = ResourceSpace - CurrResourceAmount;
                    InventoryDictionary[ItemType.Wood] += AmountAvailable;
                    CurrResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Stone:
                if (CurrResourceAmount + res.currentStackAmount <= ResourceSpace)
                {
                    InventoryDictionary[ItemType.Stone] += res.currentStackAmount;
                    CurrResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = ResourceSpace - CurrResourceAmount;
                    InventoryDictionary[ItemType.Stone] += AmountAvailable;
                    CurrResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Iron:
                if (CurrResourceAmount + res.currentStackAmount <= ResourceSpace)
                {
                    InventoryDictionary[ItemType.Iron] += res.currentStackAmount;
                    CurrResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = ResourceSpace - CurrResourceAmount;
                    InventoryDictionary[ItemType.Iron] += AmountAvailable;
                    CurrResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Bone:
                if (CurrResourceAmount + res.currentStackAmount <= ResourceSpace)
                {
                    InventoryDictionary[ItemType.Bone] += res.currentStackAmount;
                    CurrResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = ResourceSpace - CurrResourceAmount;
                    InventoryDictionary[ItemType.Bone] += AmountAvailable;
                    CurrResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Crystal:
                if (CurrResourceAmount + res.currentStackAmount <= ResourceSpace)
                {
                    InventoryDictionary[ItemType.Crystal] += res.currentStackAmount;
                    CurrResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = ResourceSpace - CurrResourceAmount;
                    InventoryDictionary[ItemType.Crystal] += AmountAvailable;
                    CurrResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Nutrition:
                if (InventoryDictionary[ItemType.Nutrition] + res.currentStackAmount <= NutritionSpace)
                {
                    InventoryDictionary[ItemType.Nutrition] += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = NutritionSpace - InventoryDictionary[ItemType.Nutrition];
                    InventoryDictionary[ItemType.Nutrition] += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }
             
          
            default:
                Debug.LogError("Resource being added is wearable, This Should not have happend");
                break;
        }
        return false;
    }
    
    public void RemoveResource(ItemType ResourceType, int amount)
    {
        switch (ResourceType)
        {
            case ItemType.Wood:
                if(InventoryDictionary[ItemType.Wood] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Wood] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Wood] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Wood] -= AmountAvailable;
                    CurrResourceAmount -= AmountAvailable;
                }
                break;
            case ItemType.Stone:
                if (InventoryDictionary[ItemType.Stone] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Stone] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Stone] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Stone] -= AmountAvailable;
                    CurrResourceAmount -= AmountAvailable;
                }
                break;
            case ItemType.Iron:
                if (InventoryDictionary[ItemType.Iron] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Iron] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Iron] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Iron] -= AmountAvailable;
                    CurrResourceAmount -= AmountAvailable;
                }
                break;
            case ItemType.Bone:
                if (InventoryDictionary[ItemType.Bone] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Bone] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Bone] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Bone] -= AmountAvailable;
                    CurrResourceAmount -= AmountAvailable;
                }
                break;
            case ItemType.Crystal:
                if (InventoryDictionary[ItemType.Crystal] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Crystal] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Crystal] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Crystal] -= AmountAvailable;
                    CurrResourceAmount -= AmountAvailable;
                }
                break;
            case ItemType.Nutrition:
                if (InventoryDictionary[ItemType.Nutrition] - amount >= 0)
                {
                    InventoryDictionary[ItemType.Nutrition] -= amount;
                    CurrResourceAmount -= amount;
                }
                else
                {
                    int difference = InventoryDictionary[ItemType.Nutrition] - amount;
                    int AmountAvailable = amount - difference;
                    InventoryDictionary[ItemType.Nutrition] -= AmountAvailable;
                }
                break;

            default:
                Debug.LogError("Resource being added is wearable, This Should not have happend");
                break;
        }
    }

}
