using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockpileFunction : BuildingFunction
{

 

    public override void Function()
    {
        //show ui elements on click
    }


    //public GameObject RemoveItem(ItemInfo item, int amount)
    //{
    //    switch (item.type)
    //    {
    //        case ItemType.Resource:
    //            if(ResourceInventory.Contains((Resource)item) )
    //            break;
    //        case ItemType.Nutrition:
    //            break;
    //        case ItemType.Weapon:
    //            break;
    //        case ItemType.Armour:
    //            break;
    //        default:
    //            break;
    //    }
         //    for (int i = 0; i < inventory.Count; i++)
         //    {
         //        if (inventory[i].itemName == item.itemName)
         //        {
         //            //if the items current stack - amount does not go below 0 and equals 0
         //            if (inventory[i].currentStackAmount - amount == 0 && inventory[i].currentStackAmount - amount > -1)
         //            {
         //                ItemInfo obj = Instantiate(inventory[i]);
         //                obj.currentStackAmount = amount;
         //                inventory.Remove(inventory[i]);
         //                Item newItem = Instantiate(itemPrefab).GetComponent<Item>();
         //                newItem.item = obj;
         //                newItem.gameObject.name = newItem.item.itemName;
         //                return newItem.gameObject;

        //            }
        //            else
        //            {
        //                int difference = amount - inventory[i].currentStackAmount;
        //                Item newItem = Instantiate(itemPrefab).GetComponent<Item>();
        //                newItem.item = Instantiate(inventory[i]);
        //                newItem.item.currentStackAmount = difference;
        //                newItem.gameObject.name = newItem.item.itemName;
        //                inventory.Remove(inventory[i]);
        //                return newItem.gameObject;
        //            }
        //        }
        //    }
        //    return null;
        //}
    }
