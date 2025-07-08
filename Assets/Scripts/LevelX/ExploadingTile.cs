using UnityEngine;

public class ExplodingTile : MonoBehaviour
{
    public float delay = 5f;
    public Material warningMaterial;
    private Material originalMaterial;
    private Renderer rend;
    private bool triggered = false;

    private float flashInterval = 0.2f;
    private float timeElapsed = 0f;
    private bool isFlashing = false;

    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        originalMaterial = rend.material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && !triggered)
        {
            Debug.Log("Player stepped on tile: " + gameObject.name);
            triggered = true;
            isFlashing = true;
            Invoke(nameof(Explode), delay);
        }
    }

    void Update()
    {
        if (isFlashing)
        {
            timeElapsed += Time.deltaTime;

            if (Mathf.FloorToInt(timeElapsed / flashInterval) % 2 == 0)
                rend.material = warningMaterial;
            else
                rend.material = originalMaterial;
        }
    }

    void Explode()
    {
        isFlashing = false;
        Destroy(gameObject);
    }
}
