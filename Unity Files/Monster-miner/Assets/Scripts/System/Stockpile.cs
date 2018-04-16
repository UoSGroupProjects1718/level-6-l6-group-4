using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stockpile : SingletonClass<Stockpile>
{
    public ResourceTypeDictionary inventoryDictionary;
    public int currResourceAmount;
    public int resourceSpace;
    public int nutritionSpace;


    public WearableInventoryDictionary wearableInventoryDictionary = new WearableInventoryDictionary();
    public int currWearablesInInventory;
    public int armourySpace;

    public override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        //give unlimited resources when in unity editor for easy testing 
        inventoryDictionary[ItemType.Wood] = 9999;
        inventoryDictionary[ItemType.Stone] = 9999;
        inventoryDictionary[ItemType.Nutrition] = 9999;
        inventoryDictionary[ItemType.Iron] = 9999;
        inventoryDictionary[ItemType.Bone] = 9999;
        inventoryDictionary[ItemType.Crystal] = 9999;
#endif
    }

    #region Resource inventory
    public bool AddResource(Resource res)
    {

        switch (res.type)
        {
            case ItemType.Wood:
                //if we can add the resource given the space we have, increase the amount of the resource and the current amount of resources
                if (currResourceAmount + res.currentStackAmount <= resourceSpace)
                {
                    inventoryDictionary[ItemType.Wood] += res.currentStackAmount;
                    currResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = resourceSpace - currResourceAmount;
                    inventoryDictionary[ItemType.Wood] += AmountAvailable;
                    currResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Stone:
                if (currResourceAmount + res.currentStackAmount <= resourceSpace)
                {
                    inventoryDictionary[ItemType.Stone] += res.currentStackAmount;
                    currResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = resourceSpace - currResourceAmount;
                    inventoryDictionary[ItemType.Stone] += AmountAvailable;
                    currResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Iron:
                if (currResourceAmount + res.currentStackAmount <= resourceSpace)
                {
                    inventoryDictionary[ItemType.Iron] += res.currentStackAmount;
                    currResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = resourceSpace - currResourceAmount;
                    inventoryDictionary[ItemType.Iron] += AmountAvailable;
                    currResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Bone:
                if (currResourceAmount + res.currentStackAmount <= resourceSpace)
                {
                    inventoryDictionary[ItemType.Bone] += res.currentStackAmount;
                    currResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = resourceSpace - currResourceAmount;
                    inventoryDictionary[ItemType.Bone] += AmountAvailable;
                    currResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Crystal:
                if (currResourceAmount + res.currentStackAmount <= resourceSpace)
                {
                    inventoryDictionary[ItemType.Crystal] += res.currentStackAmount;
                    currResourceAmount += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = resourceSpace - currResourceAmount;
                    inventoryDictionary[ItemType.Crystal] += AmountAvailable;
                    currResourceAmount += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }

            case ItemType.Nutrition:
                if (inventoryDictionary[ItemType.Nutrition] + res.currentStackAmount <= nutritionSpace)
                {
                    inventoryDictionary[ItemType.Nutrition] += res.currentStackAmount;
                    return true;
                }
                else
                {
                    int AmountAvailable = nutritionSpace - inventoryDictionary[ItemType.Nutrition];
                    inventoryDictionary[ItemType.Nutrition] += AmountAvailable;
                    res.currentStackAmount -= AmountAvailable;
                    return false;
                }


            default:
                Debug.LogError("Resource being added is wearable, This Should not have happend");
                break;
        }
        return false;
    }
    public int RemoveResource(ItemType ResourceType, int amount)
    {
        switch (ResourceType)
        {
            case ItemType.Wood:
                if (inventoryDictionary[ItemType.Wood] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Wood] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Wood, amount);
                }
            case ItemType.Stone:
                if (inventoryDictionary[ItemType.Stone] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Stone] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Stone, amount);
                }

            case ItemType.Iron:
                if (inventoryDictionary[ItemType.Iron] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Iron] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Iron, amount);
                }

            case ItemType.Bone:
                if (inventoryDictionary[ItemType.Bone] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Bone] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Bone, amount);
                }
            case ItemType.Crystal:
                if (inventoryDictionary[ItemType.Crystal] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Crystal] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Crystal, amount);
                }
            case ItemType.Nutrition:
                if (inventoryDictionary[ItemType.Nutrition] - amount >= 0)
                {
                    inventoryDictionary[ItemType.Nutrition] -= amount;
                    currResourceAmount -= amount;
                    return amount;
                }
                else
                {
                    return ReturnAmountAvailable(ItemType.Nutrition, amount);
                }

            default:
                Debug.LogError("Resource being added is wearable, This Should not have happend");
                break;
        }
        return 0;
    }
    private int ReturnAmountAvailable(ItemType itemType, int amount)
    {
        int difference = inventoryDictionary[itemType] - amount;
        int AmountAvailable = amount - Mathf.Abs(difference);
        inventoryDictionary[itemType] -= AmountAvailable;

        if (itemType < ItemType.Nutrition)
        {
            currResourceAmount -= AmountAvailable;
            currResourceAmount = Mathf.Clamp(currResourceAmount, 0, resourceSpace);
        }

        return AmountAvailable;
    }
    #endregion

    #region Wearable Inventory
    

    public string[] WearableSlugs
    {
        get
        {
            int index = 0;
            string[] slugs = new string[wearableInventoryDictionary.Keys.Count];
            foreach(KeyValuePair<Wearable,int> wearable in wearableInventoryDictionary)
            {
                slugs[index] = wearable.Key.itemName;
                index++;
            }
            return slugs;
        }
    }

    public bool AddWearable(Wearable wearable)
    {
        if (currWearablesInInventory + 1 <= armourySpace)
        {
            if (!wearableInventoryDictionary.ContainsKey(wearable))
            {
                wearableInventoryDictionary.Add(wearable, 1);
                currWearablesInInventory++;
                return true;
            }
            else
            {
                wearableInventoryDictionary[wearable]++;
                currWearablesInInventory++;
                return true;
            }
        }
        return false;
    }
    public bool removeWearable(Wearable wearable)
    {
        if (!wearableInventoryDictionary.ContainsKey(wearable))
            return false;
        
        wearableInventoryDictionary[wearable]--;
        currWearablesInInventory--;
        if (wearableInventoryDictionary[wearable] <= 0)
            wearableInventoryDictionary.Remove(wearable);
        return true;
    }
#endregion
}
