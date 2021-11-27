using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Obstacle
{
    public int doorID;
    private RepositoryOfItems repository;
    public Animator animator;
    public override void onHitEvent()
    {
        repository = RepositoryOfItems.GetInstance();
        if(repository.CheckItem(doorID, 1))
        {
            repository.UpdateItem(doorID, -1);
            remove();
        }
    }

    protected override float onRemoveAnime()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("Open", true);
        return 0.2f;
    }
}
