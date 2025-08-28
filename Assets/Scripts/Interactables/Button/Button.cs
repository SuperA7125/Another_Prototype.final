using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Button : MonoBehaviour
{
    public Animator animator;

    public List<GameObject> ShadowPlatforms = new List<GameObject>();

    public List<ShadowDoors> ShadowDoors = new List<ShadowDoors>();

    [SerializeField] private bool isPressed = false;

    public AudioClip ButtonOnSfx;

    public AudioClip ButtonOffSfx;

    void Start()
    {
            foreach (ShadowDoors door in ShadowDoors)
            {
                door.startingPos = door.shadowDoor.transform.position;
                door.SetHight();
            }
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
            isPressed = true;
            AudioManager.Instance.PlaySFXOneShot(ButtonOnSfx);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
            AudioManager.Instance.PlaySFXOneShot(ButtonOffSfx);
        }
    }
    void TogglePlatforms()
    {
        if (isPressed)
        {
            animator.SetBool("IsActive", true);
            foreach (GameObject platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(true);
            }
        }
        else
        {
            animator.SetBool("IsActive", false);
            foreach (GameObject platform in ShadowPlatforms)
            {
                platform.gameObject.SetActive(false);
            }
        }
    }

    void ToggleDoors()
    {
        if (isPressed)
        {
            animator.SetBool("IsActive", true);
            foreach (ShadowDoors door in ShadowDoors)
            {
                Vector3 currentPos = door.shadowDoor.transform.position;
                Vector3 targetPos = new Vector3(door.startingPos.x, door.startingPos.y - door.hight, door.startingPos.z);
                door.shadowDoor.transform.position = Vector3.MoveTowards(currentPos, targetPos, door.speed * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("IsActive", false);
            foreach (ShadowDoors door in ShadowDoors)
            {
                Vector3 currentPos = door.shadowDoor.transform.position;
                Vector3 targetPos = door.startingPos;
                door.shadowDoor.transform.position = Vector3.MoveTowards(currentPos, targetPos, door.speed * Time.deltaTime);
            }
        }
    }

  

}
[System.Serializable]
public class ShadowDoors
{
    public GameObject shadowDoor;

    public Vector3 startingPos;

    public int numberOfTiles;

    public float hight;

    public float speed = 1f;

    public void SetHight()
    {
        Tilemap tilemap =  shadowDoor.GetComponent<Tilemap>();
        if (tilemap != null)
        {
            float tileHeight = tilemap.layoutGrid.cellSize.y;
            hight = tileHeight * numberOfTiles;
        }
    }
}
