using UnityEngine;

public class ShadowSpikes : MonoBehaviour
{
    private Shadow _shadowPlayer;

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.CompareTag("PlayerShadow"))
        {
            _shadowPlayer = other.GetComponent<Shadow>();
            if (_shadowPlayer != null)
            {
                _shadowPlayer.ToggleMode();
            }
        }
    }
}
