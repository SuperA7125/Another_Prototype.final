using UnityEngine;

public class FML : MonoBehaviour
{
    public GameObject GameObject;

    void Update()
    {
        transform.position = GameObject.transform.position;
    }
}
