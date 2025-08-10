using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Shadow shadow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.RespawnPlayer(other.gameObject);
        }
        else if (other.CompareTag("PlayerShadow"))
        {
            shadow = other.GetComponent<Shadow>();
            if (shadow != null)
            {
                shadow.ToggleMode();
            }
        }
    }
}
