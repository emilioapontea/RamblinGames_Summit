using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [Header("Player SFX")]
    [SerializeField] private bool playSoundEffects = true;
    public AudioSource jumpPlayer;
    public AudioSource landPlayer;
    public AudioSource deathPlayer;

    private RootMotionControl rootMotionControl;
    private bool wasGrounded = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rootMotionControl = GetComponent<RootMotionControl>();
        if (rootMotionControl != null) wasGrounded = rootMotionControl.IsGrounded;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playSoundEffects)
        {
            if (rootMotionControl.IsGrounded && !wasGrounded && landPlayer != null) landPlayer.Play();

            if (!rootMotionControl.IsGrounded && wasGrounded && jumpPlayer != null) jumpPlayer.Play();
        }

        wasGrounded = rootMotionControl.IsGrounded;
    }
}
