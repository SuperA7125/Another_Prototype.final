using UnityEngine;

public class Hazard : MonoBehaviour
{
    private void TryRespawn(Collider2D other)
    {
        var player = other.GetComponentInParent<PlayerRespawnController>();
        if (player != null) player.Respawn();
    }

    private void OnTriggerEnter2D(Collider2D other) => TryRespawn(other);
    private void OnTriggerStay2D(Collider2D other) => TryRespawn(other);
}

