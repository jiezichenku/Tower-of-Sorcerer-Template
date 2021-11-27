using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStarter : MonoBehaviour
{
    PanelManage manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = PanelManage.GetInstance();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (manager.Peek() != null)
            {
                manager.Pop();
            }
            else
            {
                manager.Push(new MenuPanel());
            }
        }
        
    }
}
