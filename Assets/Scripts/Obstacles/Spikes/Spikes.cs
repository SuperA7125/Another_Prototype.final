using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Shadow _shadowPlayer;

    private Light _lightPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _lightPlayer = other.GetComponent<Light>();
            
            if (_lightPlayer != null)
            {
                Debug.Log("_lightPlayer Found");
                _lightPlayer.StartDeathAndRespawn();
            }
        }
        else if (other.CompareTag("PlayerShadow"))
        {
            _shadowPlayer = other.GetComponent<Shadow>();
            if (_shadowPlayer != null)
            {
                _shadowPlayer.ToggleMode();
            }
        }
    }
}
