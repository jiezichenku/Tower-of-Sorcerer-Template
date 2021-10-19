using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBlockController : MonoBehaviour
{
    //singleton
    BraverStatus status;
    RepositoryOfItems repository;
    ObserverMessage message;
    //List of braver status
    List<string> attributes;
    List<int> attributeValue;
    //List of display items 
    List<string> items;
    List<int> itemID;
    List<TextMesh> attributeText;
    void Start()
    {
        Debug.Log("start");
        //Get singleton instance
        status = BraverStatus.GetInstance();
        repository = RepositoryOfItems.GetInstance();
        message = ObserverMessage.GetInstance();
        //Set the item information to display
        items = new List<string>();
        itemID = new List<int>();
        items.Add("YellowKey");
        itemID.Add(1);
        items.Add("BlueKey");
        itemID.Add(2);
        items.Add("RedKey");
        itemID.Add(3);
        //update the value
        onUpdate();
    }
    private void Update()
    {
        if (message.itemUpdate || message.braverStatusUpdate)
        {
            onUpdate();
        }
    }
    public void onUpdate()
    {
        attributes = status.getAttributes();
        attributeValue = status.getAttributeValue();
        for (int i = 0; i < attributes.Count; i++)
        {
            string textName = attributes[i] + "Value";
            GameObject ob = GameObject.Find(textName);
            TextMeshProUGUI text = ob.GetComponent<TextMeshProUGUI>();
            text.SetText(attributeValue[i].ToString());
        }
        Debug.Log(items.Count);
        for (int i = 0; i < items.Count; i++)
        {
            string textName = items[i] + "Value";
            GameObject ob = GameObject.Find(textName);
            Debug.Log(textName);
            TextMeshProUGUI text = ob.GetComponent<TextMeshProUGUI>();
            text.SetText(repository.getItem(itemID[i]).ToString());
        }
        message.braverStatusUpdate = false;
        message.itemUpdate = false;
    }
}
