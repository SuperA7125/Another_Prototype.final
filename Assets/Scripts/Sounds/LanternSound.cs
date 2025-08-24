using UnityEngine;

public class LanternSound : MonoBehaviour
{


    public AudioSource audioSource;

    public AudioClip onSound;

    public AudioClip offSound;



    public KeyCode interactKey = KeyCode.E;

    public float interactRange = 1.5f;

    private bool isOn = false;

    private Transform player;



    private void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        if (player == null) return;


        float dist = Vector2.Distance(player.position, transform.position);


        if (dist <= interactRange && Input.GetKeyDown(interactKey))

        {
            ToggleLantern();
        }
    }

    private void ToggleLantern()
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

