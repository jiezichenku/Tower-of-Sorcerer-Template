using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCrystal : Item
{
    public override void onHitEvent()
    {
        BraverStatus status = BraverStatus.GetInstance();
        status.UpdateStatus(BraverAttribute.Attack, value);
        remove();
    }
}
