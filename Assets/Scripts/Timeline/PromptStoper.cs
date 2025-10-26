using UnityEngine;
using UnityEditor.Timeline;
using UnityEngine.Playables;
using System.Collections;

public class PromptStoper : MonoBehaviour
{
    [SerializeField] private PlayableDirector _director;

    [SerializeField] private Beacon _beacon;
    private void OnEnable()
    {
        StartCoroutine(WaitForE());
    }

    IEnumerator WaitForE()
    {
        var isE = Input.GetKey(KeyCode.E);
        //Time.timeScale = 0;
        while (!isE)
        {
            yield return null;
            isE = Input.GetKey(KeyCode.E);
            _director.Evaluate();
        }
        gameObject.SetActive(false);
        _beacon.ActivateBeacon();
        //Time.timeScale = 1;
    }
}
