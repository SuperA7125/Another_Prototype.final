using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    
    public AudioSource audioSource;  
    
    public AudioClip onSound;    
    
    public AudioClip offSound;    
    

    
    public KeyCode interactKey = KeyCode.E; 

    public float interactRange = 1.5f;      

    private bool isOn = false;

    private Transform playerShadow;



    private void Start()
    {

        playerShadow = GameObject.FindGameObjectWithTag("PlayerShadow").transform;

    }

    private void Update()
    {

        if (playerShadow == null) return;

        
        float dist = Vector2.Distance(playerShadow.position, transform.position);

        if (dist <= interactRange && Input.GetKeyDown(interactKey))

        {

            ToggleSwitch();

        }
    }

    private void ToggleSwitch()
    {

        isOn = !isOn;

        if (isOn && onSound != null)
        {

            audioSource.PlayOneShot(onSound);

        }
        else if (!isOn && offSound != null)
        {

            audioSource.PlayOneShot(offSound);

        }

    }
}
