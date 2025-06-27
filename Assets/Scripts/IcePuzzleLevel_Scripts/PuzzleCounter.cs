 using TMPro;
using UnityEngine;

public class PuzzleCounter : MonoBehaviour
{
    public int puzzles; // Number of puzzles in the level
    public GameObject exitDoor; // Reference to the exit door GameObject
    private int completedBlocksCount; // Counter for completed blocks

    void Start()
    {
        this.Reset();
    }
    private void UpdateCounter()
    {
        if (completedBlocksCount < puzzles)
        {
            this.GetComponent<TextMeshProUGUI>().text = "Puzzles Solved: <color=red>" + completedBlocksCount + "</color>/<color=green>" + puzzles + "</color>";
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().text = "<color=green>All Puzzles Solved!</color> Proceed to the exit.";
            exitDoor.GetComponent<Animator>().SetBool("open", true);
        }
    }

    public void Reset()
    {
        this.completedBlocksCount = 0;
        this.UpdateCounter();
    }

    public void OnPuzzleBlockCompleted()
    {
        completedBlocksCount++;
        this.UpdateCounter();
    }
}
