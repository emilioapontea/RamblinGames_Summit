using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    public bool hasLavaMushroom = false;
    public bool hasIceMushroom = false;
    public bool hasPowerMushroom = false;

    private void Awake()
    {
        // Ensure only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
