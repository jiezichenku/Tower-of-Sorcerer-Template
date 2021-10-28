using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    void OnEnable()
    {
        if (GlobalVariables.isTransfering && SceneManager.GetActiveScene().name == GlobalVariables.targetSceneName)
        {
            SceneManager.MoveGameObjectToScene(GlobalVariables.braver, SceneManager.GetSceneByName(GlobalVariables.targetSceneName));
            GlobalVariables.braver.transform.position = GlobalVariables.targetPosition;
            GlobalVariables.isTransfering = false;
        }
    }

}
