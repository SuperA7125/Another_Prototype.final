using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Beacon : MonoBehaviour
{
    //List of Light objects that will cast shadows when the beacon is activated
    public List<GameObject> LightObjects = new List<GameObject>();

    //Camera Refrences to pan over when the beacon is activated
    public CameraController CameraController;
    public Transform StartingCamPos;

    //Light2D refrence to the _lightPlayer of the beacon itself to set active when activated
    public Light2D Light2D;


    private int _currentIndex = 0;

    private bool _isBeaconActive = false;

    private Animator _animator;
    void Start()
    {
        CameraController = FindAnyObjectByType<CameraController>();
        _animator = GetComponent<Animator>();
    }

    public void ActivateBeacon()
    {

        if (!_isBeaconActive)
        {
            StartCoroutine(ActivateBeaconSequnce());
        }
    }
    IEnumerator ActivateBeaconSequnce()
    {
        CameraController.SetOverride(StartingCamPos); 

        yield return new WaitForSeconds(0.5f);

        _isBeaconActive = true;
        _animator.Play("On");
        Light2D.enabled = true;

        yield return new WaitForSeconds(2f);

        while (_currentIndex < LightObjects.Count) //As long as there is Light object on the list conintue 
        {
            GameObject obj = LightObjects[_currentIndex]; //Get the object in the current index
            Transform cameraFocus = obj.transform.Find("CameraFocus"); //Find a child transform that the camera will Focus on
            CameraController.SetOverride(cameraFocus); //Set that as the new camera focus

            yield return new WaitForSeconds(6f); //Wait for the camera to get there

            Transform child = obj.transform.Find("ShadowObject"); //Get the objects _shadowPlayer
            child.gameObject.SetActive(true); //Set the _shadowPlayer active
            

            _currentIndex++; //Grow index

            yield return new WaitForSeconds(3f); //Wait a litte more so you can see the _shadowPlayer showing up
        }

        CameraController.ClearOverride(); //Once done reset camera to the player

    }
}

