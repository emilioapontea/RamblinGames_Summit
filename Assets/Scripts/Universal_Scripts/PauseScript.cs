/*
DEPRECATED - THIS IS THE PAUSE SCRIPT FOR MILESTONE 2
*/

using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private float defaultTimeScale;
    private bool paused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        defaultTimeScale = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Time.timeScale = paused ? defaultTimeScale : 0;
            paused = !paused;
        }
    }
}
