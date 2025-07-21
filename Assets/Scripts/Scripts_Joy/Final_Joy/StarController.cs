using UnityEngine;
using UnityEngine.SceneManagement;

public class StarController : MonoBehaviour
{
    [Header("Rotation")]
    public float rotationSpeed = 90f; // degrees per second
    public float activeDistance = 5f; // distance to player to stop twisting and scale up
    public float scaleUpFactor = 1.5f; // how much bigger when close
    public float scaleSpeed = 3f; // speed of scaling

    [Header("Positions")]
    public Transform spot1;
    public Transform spot2;
    public Transform spot3;

    [Header("Sounds")]
    public AudioClip firstCollectSound;
    public AudioClip secondCollectSound;
    public AudioClip winSound;
    public AudioSource audioSource;

    private Transform player;
    private int collectCount = 0;
    private bool isActive = false;

    private Vector3 originalScale;
    public string nextSceneName;

    void Start()
    {
        gameObject.SetActive(false); // hide star initially
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogWarning("Player tag not found in scene");
        }

        originalScale = transform.localScale;
    }

    void Update()
    {
        if (!isActive || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= activeDistance)
        {
            // Stop twisting
            // Smooth scale up
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * scaleUpFactor, Time.deltaTime * scaleSpeed);
        }
        else
        {
            // Twist continuously
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            // Smooth scale down to original size
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * scaleSpeed);
        }
    }

    // Call this when all mushrooms are collected
    public void ActivateStar()
    {
        Debug.Log("Star activated!!!");
        collectCount = 0;
        isActive = true;
        gameObject.SetActive(true);
        transform.position = spot1.position;
        transform.localScale = originalScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;

        if (other.CompareTag("Player"))
        {
            collectCount++;

            switch (collectCount)
            {
                case 1:
                    Debug.Log("First catch: Star moved to spot 2! Keep going!");
                    if (audioSource && firstCollectSound)
                        audioSource.PlayOneShot(firstCollectSound);
                    StopAllCoroutines();
                    StartCoroutine(SlideToPosition(spot2.position));
                    break;

                case 2:
                    Debug.Log("Second catch: Star moved to spot 3! Final catch!");
                    if (audioSource && secondCollectSound)
                        audioSource.PlayOneShot(secondCollectSound);
                    StopAllCoroutines();
                    StartCoroutine(SlideToPosition(spot3.position));
                    break;

                case 3:
                    Debug.Log("Final catch: You Win!");
                    if (audioSource && winSound)
                        audioSource.PlayOneShot(winSound);
                    isActive = false;
                    gameObject.SetActive(false);

                    // FindObjectOfType<MushroomManager>()?.ShowWinPanel();
                    Debug.Log("Playing next scene now" + nextSceneName);
                    SceneManager.LoadScene(nextSceneName);
                    break;
            }

            // On any catch, disappear star immediately
            // If you want to keep star visible while sliding, remove this line and rely on sliding coroutine to update position
            // Here we allow star to disappear on touch, so we disable and hide it.
            // if (collectCount < 3)
            // {
            //     gameObject.SetActive(false);
            // }
        }
    }

    private System.Collections.IEnumerator SlideToPosition(Vector3 targetPos, float duration = 1.0f)
    {
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        // Show star again for sliding
        gameObject.SetActive(true);

        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
    }
}
