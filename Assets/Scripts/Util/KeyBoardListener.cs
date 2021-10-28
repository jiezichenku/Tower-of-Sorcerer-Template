using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardListener : MonoBehaviour
{

    // Update is called once per frame
    void OnGUI()
    {
        if (Input.anyKey)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                EventCenter.Broadcast(EventType.KeyboardPress, e.keyCode.ToString());
            } 
        }
    }
}
