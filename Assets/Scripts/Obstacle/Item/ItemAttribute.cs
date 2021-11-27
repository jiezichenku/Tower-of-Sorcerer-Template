using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemAttribute
{
    public int itemID { get; private set; }
    public string itemName { get; private set; }
    public string itemDescription { get; private set; }

    public ItemAttribute(int id, string name)
    {
        itemID = id;
        itemName = name;
        itemDescription = "";
    }

    public ItemAttribute(int id, string name, string description)
    {
        itemID = id;
        itemName = name;
        itemDescription = description;
    }
}
