using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public string SceneName;

    public Light LightPlayer; //Refrence to the _lightPlayer player to play the death animation on transition

    public Shadow ShadowPlayer; 

    public List<Light2D> TutorialLights = new List<Light2D>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        LightPlayer = FindAnyObjectByType<Light>();
        ShadowPlayer = FindAnyObjectByType<Shadow>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DieAndLoadScene());
        }
    } //Set a collidor on scene that will play death animation and move scences
    
    public void DieAndLoadNextScene()
    {
        StartCoroutine(DieAndLoadSceneAfterBeacon());
    }
    private IEnumerator DieAndLoadScene()
    {
        LightPlayer.StartDeath();
        
        foreach (Light2D light in TutorialLights)
        {
            light.intensity -= 0.01f;
            yield return new WaitForSeconds(0.5f);
            light.intensity -= 0.01f;
            yield return new WaitForSeconds(0.5f);
            light.intensity -= 0.01f;
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);
        yield return null;
    }

    private IEnumerator DieAndLoadSceneAfterBeacon()
    {
        ShadowPlayer.ToggleMode();

        yield return new WaitForSeconds(3f);
        
        LightPlayer.StartDeath();

        foreach (Light2D light in TutorialLights)
        {
            if (light.intensity > 0.03) light.intensity = 0.03f;

            light.intensity -= 0.01f;
            yield return new WaitForSeconds(0.5f);
            light.intensity -= 0.01f;
            yield return new WaitForSeconds(0.5f);
            light.intensity -= 0.01f;
        }

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneName);
        yield return null;
    }
}
