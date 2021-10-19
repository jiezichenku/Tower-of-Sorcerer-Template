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
    ObserverMessage message; //Send message
    private RepositoryOfItems()
    {
        //Init attributes
        itemList = new List<int>();
        message = ObserverMessage.GetInstance();
        //Placeholder for itemList[0], because list generally start from 1.
        itemList.Add(-1);
        //Set subscribers
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
            message.itemUpdate = true;
            return;
        }
        //Check the item num after delete
        int tmp = itemList[itemID] + num;
        if (tmp >= 0)
        {
            itemList[itemID] = tmp;
            message.itemUpdate = true;
            return;
        }
    }

    private void AlertItemInfo()
    {

    }
    public int getItem(int itemID)
    {
        return itemList[itemID];
    }
}
