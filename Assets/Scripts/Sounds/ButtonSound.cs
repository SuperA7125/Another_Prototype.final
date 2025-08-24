using UnityEngine;

public class ButtonSound : MonoBehaviour
{
   
    public AudioSource audioSource;    

    public AudioClip onSound;       

    public AudioClip offSound;     
    


    private int overlapCount = 0;   

    private bool isPressed = false;

    public string playerTag = "Player";


    private void Reset()
    {
       
        Collider2D col = GetComponent<Collider2D>();

        col.isTrigger = true;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        overlapCount++;

        if (!isPressed && overlapCount > 0)
        {

            isPressed = true;

            if (onSound != null) audioSource.PlayOneShot(onSound);
           
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        overlapCount = Mathf.Max(0, overlapCount - 1);

        if (isPressed && overlapCount == 0)

        {
            isPressed = false;

            if (offSound != null) audioSource.PlayOneShot(offSound);
           
        }
    }
}
