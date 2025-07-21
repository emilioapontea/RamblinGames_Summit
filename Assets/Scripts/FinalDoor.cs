using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    public Transform gateDoor;
    public float openHeight = 5f;
    public float openSpeed = 3f;
    public float closeSpeed = 3f;
    public GameObject[] ghosts;

    private Vector3 closedPos;
    private Vector3 openedPos;

    void Start()
    {
        if (gateDoor == null)
        {
            Debug.LogError("Gate_Door not assigned!");
            return;
        }
        closedPos = gateDoor.localPosition;
        openedPos = closedPos + Vector3.up * openHeight;
    }

    void Update()
    {
        bool allGhostsInactive = true;
        foreach (GameObject ghost in ghosts)
        {
            if (ghost.activeInHierarchy)
            {
                allGhostsInactive = false;
                break;
            }
        }

        if (allGhostsInactive)
        {
            gateDoor.localPosition = Vector3.MoveTowards(gateDoor.localPosition, openedPos, openSpeed * Time.deltaTime);
        }
        else
        {
            gateDoor.localPosition = Vector3.MoveTowards(gateDoor.localPosition, closedPos, closeSpeed * Time.deltaTime);
        }
    }
}
