using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedElixir : Item
{
    public override void onHitEvent()
    {
        BraverStatus status = BraverStatus.GetInstance();
        status.UpdateStatus(BraverAttribute.Health, value);
        remove();
    }
}
