using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * These varibles
 * 
 */
public class ObserverMessage
{
    //Using singleton pattern
    private static ObserverMessage singleIntance;
    public static ObserverMessage GetInstance()
    {
        if (singleIntance == null)
        {
            singleIntance = new ObserverMessage();
        }
        return singleIntance;
    }

    public bool braverStatusUpdate;
    public bool itemUpdate;

    private ObserverMessage()
    {
        braverStatusUpdate = false;
        itemUpdate = false;
    }
}
