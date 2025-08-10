using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Vector2 lastCheckpointPos;

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLastCheckpoint(Vector2 Pos)
    {
        lastCheckpointPos = Pos;
    }

    public Vector2 GetLastCheckpoint()
    {
        return lastCheckpointPos;
    }

    public void RespawnPlayer(GameObject player)
    {
        if (lastCheckpointPos != Vector2.zero)
        {
            player.transform.position = lastCheckpointPos;
        }
    }
}
