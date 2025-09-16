using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Button : MonoBehaviour
{
    private Animator _animator;

    //List of shadow platforms that are only active while on the button
    public List<GameObject> ShadowPlatforms = new List<GameObject>();

    //List of shadow doors that move while on the button
    public List<ShadowDoors> ShadowDoors = new List<ShadowDoors>();

    [SerializeField] private bool _isPressed = false;

    //Audio Refences
    public AudioClip ButtonOnSfx;
    public AudioClip ButtonOffSfx;

    void Start()
    {
        _animator = GetComponent<Animator>();
        SetDoors();
    }

   

    void Update()
    {
        TogglePlatforms();
        ToggleDoors();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPressed = true;
            AudioManager.Instance.PlaySFXOneShot(ButtonOnSfx);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPressed = false;
            AudioManager.Instance.PlaySFXOneShot(ButtonOffSfx);
        }
    }
    void TogglePlatforms()
    {
        if (_isPressed)
        {
            _animator.SetBool("IsActive", true);
            foreach (GameObject platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(true);
            }
        }
        else
        {
            _animator.SetBool("IsActive", false);
            foreach (GameObject platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(false);
            }
        }
    }

    void ToggleDoors()
    {
        if (_isPressed)
        {
            SetDoorsActive();
        }
        else
        {
            SetDoorsDeactive();
        }
    }

    private void SetDoorsDeactive()
    {
        _animator.SetBool("IsActive", false);
        foreach (ShadowDoors door in ShadowDoors)
        {
            Vector3 currentPos = door.ShadowDoor.transform.position;
            Vector3 targetPos = door.StartingPos;
            door.ShadowDoor.transform.position = Vector3.MoveTowards(currentPos, targetPos, door.DoorMoveSpeed * Time.deltaTime);
        }
    }

    private void SetDoorsActive()
    {
        _animator.SetBool("IsActive", true);
        foreach (ShadowDoors door in ShadowDoors)
        {
            Vector3 currentPos = door.ShadowDoor.transform.position;
            Vector3 targetPos = new Vector3(door.StartingPos.x, door.StartingPos.y - door.DoorHight, door.StartingPos.z);
            door.ShadowDoor.transform.position = Vector3.MoveTowards(currentPos, targetPos, door.DoorMoveSpeed * Time.deltaTime);
        }
    }

    private void SetDoors()
    {
        foreach (ShadowDoors door in ShadowDoors)
        {
            door.StartingPos = door.ShadowDoor.transform.position;
            door.SetHight();
        }
    }

}
[System.Serializable]
public class ShadowDoors
{
    public GameObject ShadowDoor;

    public Vector3 StartingPos;

    public int NumberOfTiles;

    public float DoorHight;

    public float DoorMoveSpeed = 1f;

    public void SetHight()
    {
        Tilemap tilemap =  ShadowDoor.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            float tileHeight = tilemap.layoutGrid.cellSize.y;
            DoorHight = tileHeight * NumberOfTiles;
        }
    }
}
