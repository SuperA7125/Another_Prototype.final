using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Beacon : MonoBehaviour
{

    public List<GameObject> LightObjects = new List<GameObject>();

    public CameraController cameraController;

    public Transform startingCamPos;

    public Light2D light2D;

    public bool isPlayerNearby = false;

    public Shadow ShadowPlayer;

    int currentIndex = 0;

    Animator animator;

    bool isBeaconActive = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerShadow"))
        {
            isPlayerNearby = false;
        }
    }

    public void ActivateBeacon()
    {

        if (!isBeaconActive)
        {
            StartCoroutine(ActivateBeaconSequnce());
        }
    }
    IEnumerator ActivateBeaconSequnce()
    {
        cameraController.SetOverride(startingCamPos);

        yield return new WaitForSeconds(0.5f);

        isBeaconActive = true;
        animator.Play("On");
        light2D.enabled = true;

        yield return new WaitForSeconds(2f);

        while (currentIndex < LightObjects.Count)
        {
            GameObject obj = LightObjects[currentIndex];

            Transform cameraFocus = obj.transform.Find("CameraFocus");

            cameraController.SetOverride(cameraFocus);

            yield return new WaitForSeconds(6f);

            Transform child = obj.transform.Find("ShadowObject");

            child.gameObject.SetActive(true);
            

            currentIndex++;

            yield return new WaitForSeconds(3f);
        }

        cameraController.ClearOverride();

    }
}

