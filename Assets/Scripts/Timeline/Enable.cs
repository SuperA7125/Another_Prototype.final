using UnityEngine;

public class Enable : MonoBehaviour
{
    Light Light;

    Shadow Shadow;

    private void Awake()
    {
        Light = FindAnyObjectByType<Light>();
        Shadow = FindAnyObjectByType<Shadow>();
    }
    private void OnEnable()
    {
        EnableBoth();
    }

    private void OnDisable()
    {
        DisableBoth();
    }
    void DisableBoth()
    {
        Light.enabled = false;
        Shadow.enabled = false;
    }

    void EnableBoth()
    {
        Light.enabled = true;
        
    }
}
