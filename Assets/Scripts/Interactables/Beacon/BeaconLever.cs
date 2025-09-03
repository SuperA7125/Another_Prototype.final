using UnityEngine;

public class BeaconLever : MonoBehaviour
{
    public Beacon beacon;

    bool isPlayerNearby = false;

    bool isLeverBeenActivated = false;

    Animator animator;

    public Shadow ShadowPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby && !isLeverBeenActivated &&  ShadowPlayer.enabled)
        {
            animator.Play("LeverOn");
            beacon.ActivateBeacon();
            isLeverBeenActivated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            isPlayerNearby = false;
        }
    }
}
