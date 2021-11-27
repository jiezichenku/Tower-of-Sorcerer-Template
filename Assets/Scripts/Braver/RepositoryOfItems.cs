using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositoryOfItems
{
    //Using singleton pattern
    private static RepositoryOfItems singleIntance;
    public static RepositoryOfItems GetInstance()
    {
        if (singleIntance == null)
        {
            singleIntance = new RepositoryOfItems();
        }
        return singleIntance;
    }

    //Attributes
    private List<int> itemList; //Store items
    private RepositoryOfItems()
    {
        //Init attributes
        itemList = new List<int>();
        //Placeholder for itemList[0], because list generally start from 1.
        itemList.Add(-1);
        //Set temp storage of item information
        storeItemInfo();
    }
    public bool CheckItem(int itemID, int num)
    {
        if (itemID > itemList.Count - 1)
        {
            return false;
        }
        if (itemList[itemID] < num)
        {
            return false;
        }
        return true;
    }
    public void UpdateItem(int itemID, int num)
    {
        //Item is undefinition
        if (itemID > itemList.Count - 1)
        {
            for (int i = itemList.Count - 1; i < itemID; i++)
            {
                itemList.Add(-1);
            }
        }
        //If the first time to get the item
        if (itemList[itemID] == -1 && num > 0)
        {
            itemList[itemID] = num;
            AlertItemInfo();
            EventCenter.Broadcast(EventType.ItemUpdate);
            return;
        }
        //Check the item num after delete
        int tmp = itemList[itemID] + num;
        if (tmp >= 0)
        {
            itemList[itemID] = tmp;
            EventCenter.Broadcast(EventType.ItemUpdate);
            return;
        }
    }

    public List<int> getItemList()
    {
        List<int> list = new List<int>();
        for(int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] > 0)
            {
                list.Add(i);
            }
        }
        return list;
    }

    private void AlertItemInfo()
    {

    }
    public int getItem(int itemID)
    {
        return itemList[itemID];
    }

    private void storeItemInfo()
    {
        JObject JsonData = Model.GetItemData();
        JArray itemList = (JArray)JsonData["Info"];
        foreach (var itemInfo in itemList)
        {
            int itemID = (int)itemInfo["ItemID"];
            string itemName = (string)itemInfo["ItemName"];
            string itemDescription = (string)itemInfo["ItemDescription"];
            ItemAttribute itemAttribute = new ItemAttribute(itemID, itemName, itemDescription);
            if (GlobalVariables.itemAttributeTempStore.ContainsKey(itemID))
            {
                GlobalVariables.itemAttributeTempStore[itemID] = itemAttribute;
            }
            else
            {
                GlobalVariables.itemAttributeTempStore.Add(itemID, itemAttribute);
            }
            
        }

    }
}
