using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.AI;

using UnityEngine.UI;
public class EnemyFSM : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Interact,
        DoorOpened
    }

    public Transform[] patrolPoints;
    public float interactionDistance = 3f;
    public float patrolSpeed = 1.5f;

    private NavMeshAgent agent;
    private Transform player;
    private State currentState;
    private int currentPatrolIndex = 0;

    public GameObject thoughtBubble;
    private bool doorsOpenedByGuard = false;

    public UnityEngine.UI.Image speechBubbleImage;
    public Sprite introBubbleSprite;
    public Sprite endBubbleSprite;

    void Start()
    {
        if (thoughtBubble != null)
            thoughtBubble.SetActive(false);

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = State.Patrol;

        if (patrolPoints.Length > 0)
        {
            agent.speed = patrolSpeed;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Patrol:
                if (distanceToPlayer <= interactionDistance)
                {
                    EnterInteractState();
                }
                else
                {
                    PatrolUpdate();
                }
                break;

            case State.Interact:
                if (distanceToPlayer > interactionDistance)
                {
                    ExitInteractState();
                }
                else
                {
                    FacePlayer();

                    // if all gems collected open door.
                    if (!doorsOpenedByGuard && CoinManager.Instance.collectedGems >= CoinManager.Instance.totalGems)
                    {
                        
                        CoinManager.Instance.OpenDoors();
                        doorsOpenedByGuard = true;
                        currentState = State.DoorOpened;

                        Debug.Log("Guard: Thanks for the gems! Opening the doors...");
                    }
                }
                break;

            case State.DoorOpened:
                FacePlayer(); // Continue facing player
                break;
        }
    }

    void PatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void EnterInteractState()
    {
        currentState = State.Interact;
        agent.ResetPath();
        if (thoughtBubble != null)
            UpdateSpeechBubble(CoinManager.Instance.collectedGems >= CoinManager.Instance.totalGems);
            thoughtBubble.SetActive(true);
            
       
    }

    void ExitInteractState()
    {   
        currentState = State.Patrol;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        if (thoughtBubble != null)
            thoughtBubble.SetActive(false);
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0f;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void UpdateSpeechBubble(bool hasAllGems)
    {
        if (hasAllGems)
            speechBubbleImage.sprite = endBubbleSprite;
        else
            speechBubbleImage.sprite = introBubbleSprite;

        //speechBubbleImage.gameObject.SetActive(true); // just in case it's hidden
    }
}
