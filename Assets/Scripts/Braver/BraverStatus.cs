using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class BraverStatus
{
    //Using singleton pattern
    private static BraverStatus singleIntance;
    public static BraverStatus GetInstance()
    {
        if (singleIntance == null)
        {
            singleIntance = new BraverStatus();
        }
        return singleIntance;
    }

    // private List<string> attributes;
    private Dictionary<BraverAttribute, int> attributes = new Dictionary<BraverAttribute, int>();
    public Dictionary<BraverAttribute, int> getAttributes()
    {
        return attributes;
    }
    //singleton instance
    RepositoryOfItems repository;
    private BraverStatus()
    {
        //Load init braver status
        JObject JsonData = Model.GetInitBraverData();
        foreach (BraverAttribute a in Enum.GetValues(typeof(BraverAttribute)))
        {
            string strAttribute = a.ToString();
            attributes.Add(a, (int)JsonData.GetValue(strAttribute));
        }
        //Load init braver items
        repository = RepositoryOfItems.GetInstance();
        JToken items = JsonData["Items"];
        foreach (JToken item in items)
        {
            JObject it = (JObject)item;
            int itemID = (int)it["itemID"];
            int num = (int)it["num"];
            repository.UpdateItem(itemID, num);
        }
    }

    public void UpdateStatus(BraverAttribute attribute, int num)
    {
        attributes[attribute] += num;
        EventCenter.Broadcast(EventType.StatusUpdate);
        return;
    }
}
