using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusBlockController : MonoBehaviour
{
    //singleton
    BraverStatus status = BraverStatus.GetInstance();
    RepositoryOfItems repository = RepositoryOfItems.GetInstance();
    //Dictionary of braver status
    Dictionary<BraverAttribute, int> attributes;
    //List of display items 
    List<string> items;
    List<int> itemID;
    List<TextMesh> attributeText;
    void Start()
    {

        //Set event listener
        EventCenter.AddListener(EventType.ItemUpdate, onUpdate);
        EventCenter.AddListener(EventType.StatusUpdate, onUpdate);
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

    }
    private void OnDestroy()
    {

    }
    public void onUpdate()
    {
        attributes = status.getAttributes();
        foreach (var a in attributes)
        {
            string textName = a.Key.ToString() + "Value";
            GameObject ob = GameObject.Find(textName);
            TextMeshProUGUI text = ob.GetComponent<TextMeshProUGUI>();
            text.SetText(a.Value.ToString());
        }

        for (int i = 0; i < items.Count; i++)
        {
            string textName = items[i] + "Value";
            GameObject ob = GameObject.Find(textName);
            TextMeshProUGUI text = ob.GetComponent<TextMeshProUGUI>();
            text.SetText(repository.getItem(itemID[i]).ToString());
        }
    }
}
