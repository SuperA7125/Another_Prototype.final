using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    //List Of Objects that the lantern can activat their shadows
    public List<GameObject> LightObjectWithShadows = new List<GameObject>();

    public bool IsShadowOn = false; //Checks if the stats of the shadows 
    public bool IsPlayerNearby = false; //Checks if the player is nearby

    //Refrence to check if the player is playing as _lightPlayer
    public Light LightPlayer;

    public Light2D Light2D;//The lantern Light2D to be able to set active 

    //Audio Refrences
    public AudioClip LanternOnSfx;
    public AudioClip LanternOffSfx;

    private Animator _animator;
    void Start()
    {
        LightPlayer = FindAnyObjectByType<Light>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsPlayerNearby && LightPlayer.enabled)
        {
            ToggleShadow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerNearby = false;
        }
    }

    void ToggleShadow()
    {
        if (IsShadowOn)
        {
            DeactivateShadow();

        }
        else if (!IsShadowOn)
        {
            ActivateShadow();
        }
    }

    private void ActivateShadow()
    {
        _animator.Play("On");
        Light2D.enabled = true;
        foreach (GameObject obj in LightObjectWithShadows)
        {
            foreach (Transform child in obj.transform)
            {
                IsShadowOn = true;
                child.gameObject.SetActive(true);
                AudioManager.Instance.PlaySFXOneShot(LanternOnSfx);

            }
        }
    }

    private void DeactivateShadow()
    {
        _animator.Play("Off");
        Light2D.enabled = false;
        foreach (GameObject obj in LightObjectWithShadows)
        {
            foreach (Transform child in obj.transform)
            {

                IsShadowOn = false;
                child.gameObject.SetActive(false);
                AudioManager.Instance.PlaySFXOneShot(LanternOffSfx);

            }
        }
    }
}