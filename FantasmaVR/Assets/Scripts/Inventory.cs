using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.isStackable())
        {
            bool itemInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemName == item.itemName)
                {
                    inventoryItem.amount += item.amount;
                    itemInInventory = true;
                }
            }
            if (!itemInInventory)
            {
                itemList.Add(item);
            }

        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }


    public void RemoveItem(Item item)
    {
        Debug.Log("Remove Item");
        //if (item.isStackable())
        //{
        //    Item itemInInventory = null;
        //    foreach (Item inventoryItem in itemList)
        //    {
        //        if (inventoryItem.itemName == item.itemName)
        //        {
        //            inventoryItem.amount -= item.amount;
        //            itemInInventory = inventoryItem;
        //        }
        //    }
        //    if (itemInInventory != null && itemInInventory.amount <= 0)
        //    {
        //        itemList.Remove(itemInInventory);
        //    }

        //}
        //else
        //{
        //    itemList.Remove(item);
        //}
        //OnItemListChanged?.Invoke(this, EventArgs.Empty);

    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
