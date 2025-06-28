using UnityEngine;

public class IcePuzzleTriggerController : MonoBehaviour
{
    public IceBlockController[] iceBlocks; // Array of ice blocks that trigger the puzzle
    public PuzzleCounter puzzleCounter; // Reference to the puzzle counter
    private bool isTriggered;
    public bool IsTriggered
    {
        get { return isTriggered; }
    }

    void Start()
    {
        this.Reset();
    }
    void OnTriggerStay(Collider other)
    {
        IceBlockController iceBlock = other.GetComponent<IceBlockController>();
        // if (iceBlock == null) return;
        // Debug.Log("Collision with: " + iceBlock != null ? other.name + ". isSliding = " + iceBlock.IsSliding : "null.");
        if (!isTriggered && iceBlock != null && !iceBlock.IsSliding)
        {
            this.Trigger();
        }
    }

    private void Reset()
    {
        // Initialize the trigger state if needed
        isTriggered = false;
        foreach (IceBlockController block in iceBlocks)
        {
            block.Reset(); // Reset each ice block
        }
        puzzleCounter.Reset(); // Reset the puzzle counter
        GetComponentInChildren<Animator>().SetBool("isTriggered", isTriggered);
    }

    private void Trigger()
    {
        isTriggered = true;
        GetComponentInChildren<Animator>().SetBool("isTriggered", isTriggered);
        // Ice puzzle completed logic
        Debug.Log("Ice puzzle completed!");
        puzzleCounter.OnPuzzleBlockCompleted();
        foreach (IceBlockController block in iceBlocks)
        {
            block.OnPuzzleSolved();
        }
    }
}
