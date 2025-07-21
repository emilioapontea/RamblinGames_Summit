using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GhostBoss : MonoBehaviour
{
    public GameObject[] normalGhosts; // Assign in Inspector or auto-find by tag
    public GameObject bossGhost;      // Assign in Inspector
    public GameObject dialogPanel;    // UI Panel
    public TMP_Text dialogText;       // TMP text

    private bool bossSpawned = false;
    public Transform cameraPos; // Drag "CameraPos" here in Inspector

    // Camera zoom target (set these in Inspector or code)
    public Vector3 zoomOutPosition; // Local or world position
    public float zoomDuration = 2f; // Seconds to zoom


    void Start()
    {
        bossGhost.SetActive(false);
        dialogPanel.SetActive(false);

    }

    void Update()
    {
        if (!bossSpawned && AllGhostsDefeated())
        {
            SpawnBoss();
        }
    }

    bool AllGhostsDefeated()
    {
        foreach (var ghost in normalGhosts)
        {
            if (ghost.activeInHierarchy)
                return false;
        }

        return true;
    }

    void SpawnBoss()
    {
        bossSpawned = true;
        bossGhost.SetActive(true);

        dialogText.text = "A boss ghost has appeared!";
        dialogPanel.SetActive(true);
        StartCoroutine(ZoomOutCamera());

        Invoke(nameof(HideDialog), 5f); // Hide dialog after 5 seconds
    }

    void HideDialog()
    {
        dialogPanel.SetActive(false);
    }

    IEnumerator ZoomOutCamera()
    {
    Vector3 startPos = cameraPos.position;
    Vector3 endPos = new Vector3(startPos.x, startPos.y + 2f, startPos.z - 2.5f);

    float elapsed = 0f;
    while (elapsed < zoomDuration)
    {
        cameraPos.position = Vector3.Lerp(startPos, endPos, elapsed / zoomDuration);
        elapsed += Time.deltaTime;
        yield return null;
    }
    cameraPos.position = endPos;
    }
}