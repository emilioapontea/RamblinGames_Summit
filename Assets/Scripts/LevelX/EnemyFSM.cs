using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Chase
    }

    public Transform[] patrolPoints;
    public float chaseDistance = 10f;
    public float patrolSpeed = 1.5f;
    public float chaseSpeed = 1.5f;

    private NavMeshAgent agent;
    private Transform player;
    private State currentState;
    private int currentPatrolIndex = 0;

    public GameObject thoughtBubble;

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
                if (distanceToPlayer <= chaseDistance)
                {
                    currentState = State.Chase;
                    agent.speed = chaseSpeed;
                    if (thoughtBubble != null)
                        thoughtBubble.SetActive(true);
                }
                else
                {
                    PatrolUpdate();
                }
                break;

            case State.Chase:
                if (distanceToPlayer > chaseDistance)
                {
                    currentState = State.Patrol;
                    agent.speed = patrolSpeed;
                    agent.SetDestination(patrolPoints[currentPatrolIndex].position);

                    if (thoughtBubble != null)
                        thoughtBubble.SetActive(false);
                }
                else
                {
                    ChaseUpdate();
                }
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

    void ChaseUpdate()
    {
        agent.SetDestination(player.position);
    }
}
