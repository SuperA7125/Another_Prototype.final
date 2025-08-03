using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Lantern : MonoBehaviour
{

    public List<GameObject> LightObjectWithShadows = new List<GameObject>();

    public bool isShadowOn = false;

    public bool isPlayerNearby = false;

    public Light lightPlayer;

    public TextMeshProUGUI interactText;

    void Start()
    {
        interactText.transform.position = transform.position + new Vector3(0, 1.2f, 0);
        interactText.transform.localScale = Vector3.one * 0.03f;
        interactText.text = "";
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
            interactText.text = "Press E"; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            isPlayerNearby = false;
            interactText.text = "";
        }
    }

    void ToggleShadow()
    {
        if (isShadowOn)
        {
            foreach (GameObject obj in LightObjectWithShadows)
            {
                foreach (Transform child in obj.transform)
                {
                    isShadowOn = false;
                    child.gameObject.SetActive(false);
                }
            }

        }
        else if (!isShadowOn)
        {
            foreach (GameObject obj in LightObjectWithShadows)
            {
                foreach (Transform child in obj.transform)
                {
                    isShadowOn = true;
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}