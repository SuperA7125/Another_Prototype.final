using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartScene()
    {
        // Check if a checkpoint position was saved in GameManager
        if (GameManager.Instance != null && GameManager.Instance.GetLastCheckpoint() != default)
        {
            RespawnFromCheckpoint();
        }
        else
        {
            // No checkpoint saved → reload entire scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }

    private void RespawnFromCheckpoint()
    {
        // Find the player in the scene
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // If found, move them to the checkpoint position
        if (player != null)
        {
            GameManager.Instance.RespawnPlayer(player);

            Debug.Log("Player respawned at checkpoint!");
        }
        else
        {
            // If no player found (e.g. player destroyed), reload the scene instead
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}