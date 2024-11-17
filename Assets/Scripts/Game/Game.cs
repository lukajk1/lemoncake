using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool isPaused = false;
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
