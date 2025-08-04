using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator animator;

    public List<GameObject> LightObjectToToggle = new List<GameObject>();

    public bool isLeverOn = true;

    public bool isPlayerNearby;

    public Shadow shadowPlayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby && shadowPlayer.enabled)
        {
            ToggleLight();
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

    void ToggleLight()
    {
        if (isLeverOn)
        {
            animator.SetBool("IsLeverActive",true);
            foreach (GameObject obj in LightObjectToToggle)
            {
                obj.SetActive(false);
                isLeverOn = false;
            }
        }
        else
        {
            animator.SetBool("IsLeverActive", false);
            foreach (GameObject obj in LightObjectToToggle)
            {
                obj.SetActive(true);
                isLeverOn = true;
            } 
        }    
    }
}
