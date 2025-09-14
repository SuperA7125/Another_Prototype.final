using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public string SceneName;

    public Light LightPlayer;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DieAndLoadScene());
        }
    }
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
