using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private Animator _animator;

    //List of light objects that can be toggled by the 
    public List<GameObject> LightObjectToToggle = new List<GameObject>();

    //Booleans that check if the player is playing as shadow and is nearby
    public bool IsLeverOn = false;
    public bool IsPlayerNearby;

    public Shadow ShadowPlayer;//Refrence to the shadow player

    //Audio Refrences
    public AudioClip LeverSfx;

    void Start()
    {
        ShadowPlayer = FindAnyObjectByType<Shadow>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearby && ShadowPlayer.enabled)
        {
            ToggleLight();

            AudioManager.Instance.PlaySFXOneShot(LeverSfx);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            IsPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            IsPlayerNearby = false;
        }
    }

    void ToggleLight()
    {
        if (IsLeverOn)
        {
            SetLightOff();
        }
        else
        {
            SetLightOn();
        }
    }

    private void SetLightOn()
    {
        _animator.Play("LeverOn");
        foreach (GameObject obj in LightObjectToToggle)
        {
            obj.SetActive(true);
        }
        IsLeverOn = true;
    }

    private void SetLightOff()
    {
        _animator.Play("LeverOff");
        foreach (GameObject obj in LightObjectToToggle)
        {
            obj.SetActive(false);
        }
        IsLeverOn = false;
    }
}
