using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cameraObj; 
    public bool isFollowingLight = true;

    public GameObject lightPlayer;
    public GameObject shadowPlayer;

    private Vector3 targetPos;
    public float followSpeed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckActivePlayer();

        if (isFollowingLight)
        {
            FollowToLight();
        }
        else
        {
            FollowToShadow();
        }
    }

    void FollowToLight()
    {
       targetPos = lightPlayer.transform.position;
       targetPos.z = cameraObj.transform.position.z;

       cameraObj.transform.position = Vector3.MoveTowards(cameraObj.transform.position, targetPos, followSpeed * Time.deltaTime);
 
    }

    void FollowToShadow()
    {
        targetPos = shadowPlayer.transform.position;
        targetPos.z = cameraObj.transform.position.z;

        cameraObj.transform.position = Vector3.MoveTowards(cameraObj.transform.position, targetPos, followSpeed * Time.deltaTime);
    }

    void CheckActivePlayer()
    {
        if (lightPlayer.GetComponent<Light>().enabled == false)
        {
            isFollowingLight = false;
        }
        else
        {
            isFollowingLight = true;
        }
    }
}
