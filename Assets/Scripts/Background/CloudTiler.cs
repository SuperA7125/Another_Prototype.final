using UnityEngine;

public class CloudTiler : MonoBehaviour
{
    public float speed = 0.5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10f)
        {
            transform.position = new Vector2(10f, transform.position.y);
        }
    }
}
