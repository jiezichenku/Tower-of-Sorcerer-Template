using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    public abstract void onHitEvent();
    protected virtual void remove()
    {
        Destroy(this.gameObject);
    }
    private void Start()
    {

    }
}
