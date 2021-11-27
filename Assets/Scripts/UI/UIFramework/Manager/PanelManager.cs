using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manage panels by stack structure
public class PanelManage
{
    private Stack<BasePanel> panelStack;
    private UIManager UIManager;
    private BasePanel panel;

    private static PanelManage singleIntance;
    public static PanelManage GetInstance()
    {
        if (singleIntance == null)
        {
            singleIntance = new PanelManage();
        }
        return singleIntance;
    }

    private PanelManage()
    {
        panelStack = new Stack<BasePanel>();
        UIManager = new UIManager();
    }

    // Push panel into stack to display the panel
    public void Push(BasePanel nextPanel)
    {
        if (panelStack.Count > 0)
        {
            panel = panelStack.Peek();
            panel.OnPause();
        }
        panelStack.Push(nextPanel);
        GameObject panelObject = UIManager.GetSingleUI(nextPanel.Type);
        UITool.GetInstance().activePanel = panelObject;
        nextPanel.OnEnter();
    }

    // Pop panel to dismiss the panel
    public void Pop()
    {
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnExit();
            UIManager.DestroyUI(panelStack.Peek().Type);
            panelStack.Pop();
            
        }
        if (panelStack.Count > 0)
        {
            panelStack.Peek().OnResume();
        }
    }

    // Get peek element
    public BasePanel Peek()
    {
        if (panelStack.Count > 0)
        {
            return panelStack.Peek();
        }
        else
        {
            return null;
        }
    }
}
