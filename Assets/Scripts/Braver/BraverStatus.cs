using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

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

    // Attributes
    private List<string> attributes;
    public List<string> getAttributes()
    {
        return attributes;
    }
    private List<int> attributeValue;
    public List<int> getAttributeValue()
    {
        return attributeValue;
    }
    //singleton instance
    RepositoryOfItems repository;
    ObserverMessage message;
    private BraverStatus()
    {
        //Init braver status list
        attributes = new List<string>();
        attributeValue = new List<int>();
        attributes.Add("Floor");
        attributes.Add("Health");
        attributes.Add("Attack");
        attributes.Add("Defence");
        attributes.Add("Shield");
        attributes.Add("Experience");
        attributes.Add("Gold");
        //Load init braver status
        JObject status = Model.GetInitBraverData();
        foreach (string attribute in attributes)
        {
            attributeValue.Add((int)status[attribute]);
        }
        //Load init braver items
        repository = RepositoryOfItems.GetInstance();
        JToken items = status["Items"];
        foreach (JToken item in items)
        {
            JObject it = (JObject)item;
            int itemID = (int)it["itemID"];
            int num = (int)it["num"];
            repository.UpdateItem(itemID, num);
        }
        //Load message
        message = ObserverMessage.GetInstance();
    }
}
