using System;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static bool isPaused;
    public static bool IsPaused 
    { 
        get
        {
            return isPaused;
        }
        set
        {
            isPaused = value;
            PauseUpdated?.Invoke(value);
        }
    }
    public static Action<bool> PauseUpdated;
    public static Game I { get; private set; }

    private void Awake()
    {
        if (I != null) 
        {
            Debug.LogError($"More than one instance of {I} in scene");
        }

        I = this;
    }
}
