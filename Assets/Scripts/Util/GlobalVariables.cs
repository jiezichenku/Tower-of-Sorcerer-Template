using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    // Pointer to braver object
    public static GameObject braver;
    // Pointer to UI canvas
    public static GameObject UI;

    // Get current event name
    public static string currentEventName;

    // Game map lists
    public static List<Map> maps = new List<Map>();

    // Braver transfer
    public static bool isTransfering;
    public static string targetSceneName;
    public static Vector2 targetPosition;
}
