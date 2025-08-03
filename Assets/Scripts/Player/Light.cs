using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public class Light : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpForce = 1f;

    public bool hasJumped = false;

    public GameObject shadowObj;

    Shadow shadowScript;

    public Rigidbody2D lightRb;

    public LayerMask lightGround;

    public float rayLength;

    private float horizontal;
    void Start()
    {
        shadowScript = shadowObj.GetComponentInChildren<Shadow>();
        shadowScript.enabled = false;
        
    }


    void Update()
    {
        GroundCheck();

        SetShadowPos();

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Key Down!");
            ToggleMode();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
        {
            hasJumped = true;
            lightRb.AddForce(Vector3.up * jumpForce , ForceMode2D.Impulse);
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
        if (!shadowScript.enabled)
        {
            shadowScript.enabled = true;
            this.enabled = false;
        }
    }

    void SetShadowPos()
    {
        shadowObj.transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y,-1);
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, lightGround);

        /*Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
        Debug.Log(hit.ToString());*/
        //Debug.Log(hit.collider);
        if (hit.collider != null )
        {
            hasJumped = false;
            return;
        }
    }
}
