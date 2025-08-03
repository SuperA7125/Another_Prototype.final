using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpForce = 1f;   

    bool hasJumped = false;
    public GameObject lightObj;

    Light lightScript;

    public Rigidbody2D shadowRb;

    public LayerMask shadowGround;

    public float rayLength;

    private float horizontal;
    void Start()
    {
        lightScript = lightObj.GetComponentInChildren<Light>();
        
    }


    void Update()
    {
        GroundCheck();

        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.green);

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Key Down!");
            ToggleMode();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            hasJumped = true;
            shadowRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }
        horizontal = Input.GetAxis("Horizontal");

        
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            transform.position += Vector3.right * (horizontal * speed * Time.deltaTime);

        }
    }
    void ToggleMode()
    {
        if (!lightScript.enabled)
        {
            this.transform.position = new Vector3(lightObj.transform.position.x -0.2f , lightObj.transform.position.y );

            lightScript.enabled = true;
            this.enabled = false;
        }
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, shadowGround);

        /*Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
        Debug.Log(hit.ToString());*/
        if (hit.collider != null)
        {
            hasJumped = false;
            return;
        }
    }
}