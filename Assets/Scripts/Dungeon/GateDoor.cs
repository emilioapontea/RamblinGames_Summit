using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public Transform gateDoor;         
    public float openHeight = 5f;
    public float openSpeed = 3f;       
    public float closeSpeed = 3f;     

    private Vector3 closedPos;
    private Vector3 openedPos;
    private bool isOpen = false;
    private bool hasClosed = false;

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

    void OnTriggerEnter(Collider other)
    {
        if (hasClosed) return;
        if (other.CompareTag("Player"))
        {
            isOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hasClosed) return;
        if (other.CompareTag("Player"))
        {
            isOpen = false;
            hasClosed = true; // Door will close and never open again
        }
    }

    void Update()
    {
        if (isOpen && !hasClosed)
        {
            gateDoor.localPosition = Vector3.MoveTowards(gateDoor.localPosition, openedPos, openSpeed * Time.deltaTime);
        }
        else
        {
            gateDoor.localPosition = Vector3.MoveTowards(gateDoor.localPosition, closedPos, closeSpeed * Time.deltaTime);
        }
    }
}
