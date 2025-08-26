using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Beacon : MonoBehaviour
{

    public List<GameObject> LightObjects = new List<GameObject>();

    public CameraController cameraController;

    public Transform startingCamPos;
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
        if (Input.GetKeyDown(KeyCode.E) && isPlayerNearby && ShadowPlayer.enabled && !isBeaconActive)
        {
            StartCoroutine(ActivateBeaconSequnce());
        }
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

    void ActivateShadow()
    {
        foreach (GameObject obj in LightObjects)
        {
            foreach (Transform child in obj.transform)
            {
                child.gameObject.SetActive(true);

            }
        }
    }

    IEnumerator ActivateBeaconSequnce()
    {
        cameraController.SetOverride(startingCamPos);

        yield return new WaitForSeconds(0.5f);

        isBeaconActive = true;
        animator.Play("On");

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

