using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;

    public List<GameObject> ShadowPlatforms = new List<GameObject>();

    public List<GameObject> ShadowDoors = new List<GameObject>();

    [SerializeField] private bool isPressed = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        TogglePlatforms();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
        }
    }
    void TogglePlatforms()
    {
        if (isPressed)
        {
            animator.SetBool("IsActive", true);
            foreach (var platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(true);
            }
        }
        else
        {
            animator.SetBool("IsActive", false);
            foreach (var platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(false);
            }
        }
    }

    void ToggleDoors()
    {

    }
}
