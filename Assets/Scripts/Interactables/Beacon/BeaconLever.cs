using UnityEngine;

public class BeaconLever : MonoBehaviour
{
    //Refrence to the beacon that needs to be activated
    public Beacon beacon;

    //Booleans to check if the player is close and make sure the lever can only be activated and not turned off
    private bool _isPlayerNearby = false;
    private bool _isLeverBeenActivated = false;

    private Animator _animator;

    public Shadow ShadowPlayer; //Refeance to check if the player is playing as the _shadowPlayer or _lightPlayer

    void Start()
    {
        ShadowPlayer = FindAnyObjectByType<Shadow>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckPlayerActivatingLever();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            _isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            _isPlayerNearby = false;
        }
    }

    private void CheckPlayerActivatingLever()
    {
        if (Input.GetKeyDown(KeyCode.E) && _isPlayerNearby && !_isLeverBeenActivated && ShadowPlayer.enabled)
        {
            _animator.Play("LeverOn");
            beacon.ActivateBeacon();
            _isLeverBeenActivated = true;
        }
    }

}
