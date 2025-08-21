using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Shadow shadow;

    private Light light;
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
            light = other.GetComponent<Light>();
            
            if (light != null)
            {
                Debug.Log("light Found");
                light.StartDeathAndRespawn();
            }
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
