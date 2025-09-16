using TMPro;
using UnityEngine;

public class TutorialTextShow : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public bool StartVisible = false;

    private void Start()
    {
        
        Text.enabled = StartVisible;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Text.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Text.enabled = false;
        }
    }
}
