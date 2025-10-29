using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class CutscenceMove : MonoBehaviour
{
   [SerializeField] PlayableDirector _diractor1;
   [SerializeField] GameObject _diractor2;



    private void OnEnable()
    {
        StartCoroutine(WaitForE());
    }

    IEnumerator WaitForE()
    {
        var isE = Input.GetKey(KeyCode.E);
        while (!isE)
        {
            yield return null;
            isE = Input.GetKey(KeyCode.E);

        }

        _diractor1.Stop();
        _diractor2.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
