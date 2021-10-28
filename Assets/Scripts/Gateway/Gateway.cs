using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway : MonoBehaviour
{
    public string targetSceneName;
    public Vector2 targetPosition;
    private Map currentMap;
    private void Awake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Vector2 currentPosition = transform.position;
        string mapName = Regex.Replace(currentSceneName, @"-?\d", "");
        currentMap = Map.MapInstance(mapName);
        currentMap.AddGateway(currentSceneName, currentPosition, targetSceneName, targetPosition);
    }
    public void onHitEvent()
    {
        //Get current floor
        string currentSceneName = SceneManager.GetActiveScene().name;
        //Move braver to new floor
        try 
        {
            //Protect object
            DontDestroyOnLoad(GlobalVariables.braver);
            DontDestroyOnLoad(GlobalVariables.UI);
            //Load new scene
            SceneManager.LoadScene(targetSceneName);
            //Transfer braver
            GlobalVariables.isTransfering = true;
            GlobalVariables.targetSceneName = targetSceneName;
            GlobalVariables.targetPosition = targetPosition;
        }
        catch(UnityException e)
        {
            Debug.LogError(e);
        }
    }
}
