using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 spawnPos;          // last checkpoint (or start)
    private float respawnCooldownUntil;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // default spawn is where the player starts
        spawnPos = rb.position;
        Debug.Log("[Player] Start spawn set to " + spawnPos);
    }

    public void SetCheckpoint(Vector2 pos)
    {
        spawnPos = pos;
        Debug.Log("[Player] Checkpoint set: " + spawnPos);
    }

    public void Respawn()
    {
        // tiny cooldown so we don't immediately re-trigger hazards on spawn
        if (Time.time < respawnCooldownUntil) return;

        rb.linearVelocity = Vector2.zero;
        rb.position = spawnPos;
        respawnCooldownUntil = Time.time + 0.05f;
        Debug.Log("[Player] Respawned at " + spawnPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Touching a checkpoint?
        var cp = other.GetComponent<Checkpoint>();
        if (cp != null)
        {
            SetCheckpoint(cp.GetSpawnPosition());
        }
    }
}
