using UnityEngine;
using System.Collections;
public class BeaconsManager : MonoBehaviour
{

    public int ActiveBeacons;

    public GameObject Beacon1;
    public GameObject Beacon2;
    public GameObject Beacon3;

    private Animator _animator1;
    private Animator _animator2;
    private Animator _animator3;

    private void Start()
    {
        ActiveBeacons = GameManager.Instance.ActiveBeaconsCount;
        _animator1 = Beacon1.GetComponent<Animator>();
        _animator2 = Beacon2.GetComponent<Animator>();
        _animator3 = Beacon3.GetComponent<Animator>();
        ActivateBeacons();
    }


    private void ActivateBeacons()
    {
        switch (ActiveBeacons)
        {
            case 0:
                return;
            case 1:
                _animator1.Play("Beacon Light Up");
                StartCoroutine(WaitAndGoToScene(2, "TestLevel"));
                return;
            case 2:
                _animator1.Play("BeaconActiveAlready");
                _animator2.Play("Beacon Light Up");
                return;
            case 3:
                _animator1.Play("BeaconActiveAlready");
                _animator2.Play("BeaconActiveAlready");
                _animator3.Play("Beacon Light Up");
                return;
        }
    }
    
    IEnumerator WaitAndGoToScene(int x, string sceneName)
    {
        yield return new WaitForSeconds(x);
        ScenesManager.Instance.LoadSceneFromString(sceneName);
    }
}
