using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector2 _lastCheckpointPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLastCheckpoint(Vector2 Pos)
    {
        _lastCheckpointPos = Pos;
    }

    public Vector2 GetLastCheckpoint()
    {
        return _lastCheckpointPos;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (_lastCheckpointPos != default)
        {
            player.transform.position = new Vector3(_lastCheckpointPos.x, _lastCheckpointPos.y + 0.5f,0);
        }
    }
}
