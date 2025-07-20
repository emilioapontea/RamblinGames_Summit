using UnityEngine;

public class WallslidePlayer : MonoBehaviour
{
    /// <summary>
    /// The audio source that plays a sound effect while the player is
    /// airborne and in contact with a wall that can be walljumped off of.
    /// </summary>
    public AudioSource SlideSoundEffect;
    /// <summary>
    /// Play and loop the sliding sound effect while true.
    /// Stop playing the sound effect when false.
    /// </summary>
    private int walljumpWallContactCount = 0;
    private PlayerController playerControllerScript;
    void Awake()
    {
        if (SlideSoundEffect == null) Debug.LogError("WallslidePlayer: No sliding sound effect found.");
        playerControllerScript = GetComponent<PlayerController>();
        if (playerControllerScript == null) Debug.LogError("WallslidePlayer: No PlayerController script found in this object");
    }

    void Update()
    {
        // Start playing the sliding sound effect if all of the following are true:
        // - The sliding sound effect is not currently playing
        // - The player is in contact with a wall that can be walljumped off of
        // - The player is currently airborne
        if (!SlideSoundEffect.isPlaying && walljumpWallContactCount > 0 && !playerControllerScript.GetGrounded())
        {
            // Start playing the sliding sound effect
            SlideSoundEffect.Play();
        }
        // Stop playing the sliding sound effect if the sound effect is currently playing 
        // and one or more of the following are true:
        // - The player is not in contact with a wall that can be walljumped off of
        // - The player is not currently airborne
        else if (SlideSoundEffect.isPlaying && (walljumpWallContactCount <= 0 || playerControllerScript.GetGrounded()))
        {
            // Stop playing the sliding sound effect
            SlideSoundEffect.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount++;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("WalljumpWall"))
        {
            walljumpWallContactCount--;
        }
    }
}
