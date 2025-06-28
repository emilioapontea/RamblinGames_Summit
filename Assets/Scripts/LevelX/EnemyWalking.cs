using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
public class EnemyWalking : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;

    public float damage = 1f;
    public AudioClip hitSound;
    public float hitCooldown = 1f;

    private NavMeshAgent agent;
    private float timer;
    private float lastHitTime = -999f;

    private AudioSource audioSource;

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask))
        {
            return navHit.position;
        }

        return origin; // fallback
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time - lastHitTime > hitCooldown)
        {
            lastHitTime = Time.time;

            if (hitSound && audioSource)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (PlayerStats.Instance != null)
            {
                PlayerStats.Instance.TakeDamage(damage);
            }
        }
    }
}
