using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public string SceneName;

    public Light LightPlayer; //Refrence to the _lightPlayer player to play the death animation on transition

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

        LightPlayer = FindAnyObjectByType<Light>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DieAndLoadScene());
        }
    } //Set a collidor on scene that will play death animation and move scences
    public void ChangeScence()
    {
        SceneManager.LoadScene(SceneName);
    }
    
    private IEnumerator DieAndLoadScene()
    {
        LightPlayer.StartDeath();
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(SceneName);
        yield return null;
    }
}
