using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{

    public List<GameObject> LightObjectWithShadows = new List<GameObject>();

    public bool isShadowOn = false;

    public bool isPlayerNearby = false;

    public Light lightPlayer;

    public Light2D light2D;

    public TextMeshProUGUI interactText;

    public AudioClip LanternOnSfx;

    public AudioClip LanternOffSfx;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        /*interactText.transform.position = transform.position + new Vector3(0, 1.2f, 0);
        interactText.transform.localScale = Vector3.one * 0.03f;
        interactText.text = "";*/
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby && lightPlayer.enabled)
        {
            ToggleShadow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            if (interactText != null)
            {
                interactText.text = "Press E";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactText != null)
            {
                interactText.text = "";
            }
        }
    }

    void ToggleShadow()
    {
        if (isShadowOn)
        {
            animator.Play("Off");
            light2D.enabled = false;
            foreach (GameObject obj in LightObjectWithShadows)
            {
                foreach (Transform child in obj.transform)
                {

                    isShadowOn = false;
                    child.gameObject.SetActive(false);
                    AudioManager.Instance.PlaySFXOneShot(LanternOffSfx);

                }
            }

        }
        else if (!isShadowOn)
        {
            animator.Play("On");
            light2D.enabled = true;
            foreach (GameObject obj in LightObjectWithShadows)
            {
                foreach (Transform child in obj.transform)
                {
                    isShadowOn = true;
                    child.gameObject.SetActive(true);
                    if (AudioManager.Instance) AudioManager.Instance.PlaySFXOneShot(LanternOnSfx);

                }
            }
        }
    }
}