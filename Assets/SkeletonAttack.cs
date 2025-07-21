using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float attackInterval = 1.5f;
    public int attackDamage = 1;

    private float attackTimer = 0f;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        attackTimer += Time.deltaTime;

        if (distance <= attackRange && attackTimer >= attackInterval)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                ph.TakeDamage(attackDamage);
            }
            attackTimer = 0f;
        }
    }
}