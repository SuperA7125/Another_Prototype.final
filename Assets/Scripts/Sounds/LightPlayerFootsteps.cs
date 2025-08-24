using UnityEngine;

public class LightPlayerFootsteps : MonoBehaviour
{

    [Header("Audio")]
    public AudioSource audioSource;        // Drag your AudioSource here (or it will auto-grab on Awake)
    public AudioClip footstepClip;         // Single footstep sound
    public float stepInterval = 0.4f;      // Time between steps

    [Header("Movement Detection")]
    public Rigidbody2D rb;                 // Optional. Assign your player's Rigidbody2D if you have one
    public string horizontalAxis = "Horizontal";
    public string jumpButton = "Jump";
    public float minHorizontalSpeed = 0.1f; // How much x-speed counts as "moving"
    public float nearZeroYSpeed = 0.05f;    // Treat |vy| < this as "not going up/down"

    private float stepTimer = 0f;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        bool movingHoriz = false;
        bool notUpOrDown = true;

        // Prefer Rigidbody2D if provided
        if (rb != null)
        {
            movingHoriz = Mathf.Abs(rb.linearVelocity.x) > minHorizontalSpeed;
            notUpOrDown = Mathf.Abs(rb.linearVelocity.y) < nearZeroYSpeed;
        }
        else
        {
            // Fallback: use input axis (works for transform-based movement)
            float inputX = Input.GetAxisRaw(horizontalAxis);
            movingHoriz = Mathf.Abs(inputX) > 0.01f;

            // Best-effort “not in air” without a ground check:
            // Don’t play if the jump button is being held this frame
            notUpOrDown = !Input.GetButton(jumpButton);
        }

        if (movingHoriz && notUpOrDown)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                if (footstepClip != null && audioSource != null)
                {
                    audioSource.PlayOneShot(footstepClip);
                }
                stepTimer = stepInterval;
            }
        }
        else
        {
            // Reset so the next time you start walking it plays immediately
            stepTimer = 0f;
        }
    }
}
