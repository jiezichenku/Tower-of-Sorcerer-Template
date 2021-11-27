using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCrystal : Item
{
    public override void onHitEvent()
    {
        BraverStatus status = BraverStatus.GetInstance();
        status.UpdateStatus(BraverAttribute.Defence, value);
        remove();
    }
}
