using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera CameraObj; 
    public bool IsFollowingLight = true; //Check if the camera is following the Light player

    //References to the _lightPlayer and _shadowPlayer game object for the camera to follow
    public GameObject LightPlayer;
    public GameObject ShadowPlayer;

    //Camera follow settings
    public float YOffset = 1f;
    private Vector3 _targetPos;
    public float FollowSpeed = 5f;

    private Transform _overrideTarget = null;

    void FixedUpdate()
    {
        SetCameraFollow();
    }

    private void SetCameraFollow()
    {
        if (_overrideTarget != null)
        {
            FollowToTarget();
        }
        else
        {
            CheckActivePlayer();

            if (IsFollowingLight)
            {
                FollowToLight();
            }
            else
            {
                FollowToShadow();
            }
        }
    }

    void FollowToLight()
    {
       _targetPos = LightPlayer.transform.position + new Vector3(0, YOffset, 0); 
       _targetPos.z = CameraObj.transform.position.z;

       CameraObj.transform.position = Vector3.MoveTowards(CameraObj.transform.position, _targetPos, FollowSpeed * Time.deltaTime);
 
    }

    void FollowToShadow()
    {
        _targetPos = ShadowPlayer.transform.position + new Vector3(0, YOffset, 0);
        _targetPos.z = CameraObj.transform.position.z;

        CameraObj.transform.position = Vector3.MoveTowards(CameraObj.transform.position, _targetPos, FollowSpeed * Time.deltaTime);
    }

    void FollowToTarget()
    {
        _targetPos = _overrideTarget.transform.position;
        _targetPos.z = CameraObj.transform.position.z;

        CameraObj.transform.position = Vector3.MoveTowards(CameraObj.transform.position, _targetPos, FollowSpeed * Time.deltaTime);
    }

    public void SetOverride(Transform transform) 
    { 
        _overrideTarget = transform; 
    }

    public void ClearOverride()
    {
        _overrideTarget = null;
    }
    void CheckActivePlayer()
    {
        if (LightPlayer.GetComponent<Light>().enabled == false)
        {
            IsFollowingLight = false;
        }
        else
        {
            IsFollowingLight = true;
        }
    }
}
