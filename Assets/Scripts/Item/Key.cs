using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    private RepositoryOfItems repository;
    public override void onHitEvent()
    {
        repository = RepositoryOfItems.GetInstance();
        repository.UpdateItem(itemID, 1);
        remove();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
