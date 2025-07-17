using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// For custom text area in the inspector
[System.Serializable]
public class DialogLine
{
    [TextArea]
    public string line;
}


public class DialogManager : MonoBehaviour
{
    public float delay = 1f; // Delay before showing the next dialog line
    public float enterDelay = 5f; // Delay before showing the "Press Enter" prompt
    private float enterTimer; // Timer for the "Press Enter" prompt
    public List<DialogLine> dialogLines;
    public TextMeshProUGUI dialogText;
    public TextMeshProUGUI enterText; // Text to show the "Press Enter" prompt
    private int currentLineIndex = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false); // Disable the dialog canvas until dialog is triggered
        Invoke("Next", delay); // Call Next after one second
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return) && gameObject.activeSelf)
        {
            Next(); // Show the next dialog line when space is pressed
        }

        enterTimer += Time.deltaTime;
        if (enterTimer >= enterDelay)
        {
            enterText.gameObject.SetActive(true); // Show the "Press Enter" prompt after the specified delay
        }
        else
        {
            enterText.gameObject.SetActive(false); // Hide the "Press Enter" prompt before the delay
        }
    }

    void Next()
    {
        enterTimer = 0f; // Reset the enter timer when space is pressed
        currentLineIndex++;
        if (currentLineIndex >= dialogLines.Count)
        {
            Destroy(gameObject); // Destroy the dialog manager when all lines are shown
        }
        else
        {
            gameObject.SetActive(true); // Enable the dialog canvas
            dialogText.text = dialogLines[currentLineIndex].line; // Set the dialog text to the current line
        }
    }
}
