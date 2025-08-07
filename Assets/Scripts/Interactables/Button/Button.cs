using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator animator;

    public List<GameObject> ShadowPlatforms = new List<GameObject>();

    public List<ShadowDoors> ShadowDoors = new List<ShadowDoors>();

    [SerializeField] private bool isPressed = false;

    void Start()
    {
        foreach (ShadowDoors door in ShadowDoors)
        {
            door.startingPos = door.shadowDoor.transform.position;
            door.hight = door.shadowDoor.GetComponent<SpriteRenderer>().bounds.size.y;
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
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPressed = false;
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

    public float hight;

    public float speed = 5f;
}
