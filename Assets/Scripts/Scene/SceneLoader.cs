using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    void Start()
    {
        // Move braver to current scene
        string sceneName = SceneManager.GetActiveScene().name;
        if (GlobalVariables.isTransfering && sceneName == GlobalVariables.targetSceneName)
        {
            SceneManager.MoveGameObjectToScene(GlobalVariables.braver, SceneManager.GetSceneByName(GlobalVariables.targetSceneName));
            GlobalVariables.braver.transform.position = GlobalVariables.targetPosition;
            SceneManager.MoveGameObjectToScene(GlobalVariables.UI, SceneManager.GetSceneByName(GlobalVariables.targetSceneName));
            GlobalVariables.UI.transform.position = new Vector2(0, 0);
            GlobalVariables.isTransfering = false;
        }
        // Delete destroyed objects
        if (GlobalVariables.sceneDestroyedObjects.ContainsKey(sceneName))
        {
            //if scene was loaded
            GlobalVariables.sceneDestroyedObjects.TryGetValue(sceneName, out List<Vector2>destroyedList);
            GameObject[] objects = SceneManager.GetActiveScene().GetRootGameObjects();
            //search those game objects located on destroyed lists
            foreach (GameObject o in objects)
            {
                foreach(Transform t in o.transform)
                {
                    Vector2 position = new Vector2(t.position.x, t.position.y);
                    if (destroyedList.Contains(position))
                    {
                        Destroy(t.gameObject);
                    }
                }
            }
        }
        else
        {
            //if not loaded then initialize
            GlobalVariables.sceneDestroyedObjects.Add(sceneName, new List<Vector2>());
        }
        // Update status
        EventCenter.Broadcast(EventType.StatusUpdate);
    }

}
