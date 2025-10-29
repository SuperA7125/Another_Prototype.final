using UnityEngine;
using System.Collections;

public class CutsenceActivationZone : MonoBehaviour
{
    public BoxCollider2D Zone;

    public GameObject CutscenceToActtivate;

    public GameObject Player;

    public Vector3 StartingPos;

    public CameraController CameraController;

    public GameObject CameraPos;

    private Shadow _shadowPlayer;

    private Light _lightPlayer;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _lightPlayer = other.GetComponent<Light>();
            _shadowPlayer = other.GetComponentInChildren<Shadow>();

            if (_lightPlayer != null)
            {
                _lightPlayer.enabled = false;
                _shadowPlayer.enabled = false;
                //Player.transform.position = StartingPos;
                CameraController.SetOverride(CameraPos.transform);
                AudioManager.Instance.StopFootSteps = true;
                StartCoroutine(IWaitAndActivateCutscence());
            }
        }
        else if (other.CompareTag("PlayerShadow"))
        {
            _shadowPlayer = other.GetComponent<Shadow>();
            if (_shadowPlayer != null)
            {
                _shadowPlayer.ToggleMode();
            }
        }
    }

    IEnumerator IWaitAndActivateCutscence()
    {
        yield return new WaitForSeconds(0.5f);

        CutscenceToActtivate.SetActive(true);
    }
}
