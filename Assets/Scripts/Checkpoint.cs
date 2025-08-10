using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform spawnAnchor;
    public Vector2 offset = Vector2.up * 0.5f;

    public Vector2 GetSpawnPosition()
    {
        return spawnAnchor ? (Vector2)spawnAnchor.position
                           : (Vector2)transform.position + offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GetSpawnPosition(), 0.18f);
    }
}
