 using TMPro;
using UnityEngine;

public class PuzzleCounter : MonoBehaviour
{
    public int puzzles; // Number of puzzles in the level
    public GameObject exitDoor; // Reference to the exit door GameObject
    public CentralAudioManager audioManager; // Reference to the Audio Manager GameObject
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
            exitDoor.GetComponent<AudioSource>().Play();

            // EventManager.TriggerEvent<AllPuzzlesSolvedEvent>();
            audioManager.PlaySound(0); // Play the first sound in the Audio Manager's list
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
        audioManager.PlaySound(1); // Play the second sound in the Audio Manager's list
        this.UpdateCounter();
    }
}
