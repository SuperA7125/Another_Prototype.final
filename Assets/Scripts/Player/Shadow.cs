using System.Collections;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpForce = 1f;   

    bool hasJumped = false;

    public Animator animator;

    public PlayerState state;

    private SpriteRenderer spriteRenderer;

    public GameObject lightObj;

    Light lightScript;

    public Rigidbody2D shadowRb;

    public LayerMask shadowGround;

    public float rayLength;

    private float horizontal;

    
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lightScript = lightObj.GetComponentInChildren<Light>();
        
    }

    private void OnEnable()
    {
        ResetGravity();
    }

    void Update()
    {
        if (!IsAnimationRunning("Disappear"))
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


            animator.Play(state.ToString());
        }

        Debug.Log(state.ToString());
    }

    private void FixedUpdate()
    {
        
        
            if (horizontal != 0)
            {
                state = PlayerState.Walk;
                transform.position += Vector3.right * (horizontal * speed * Time.deltaTime);

                spriteRenderer.flipX = horizontal > 0;
            }
            else
            {
                    state = PlayerState.Idle; 
            }
        
    }
    public void ToggleMode()
    {
        if (!lightScript.enabled)
            StartCoroutine(SwitchToLightAfterAnimation());
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, shadowGround);

        /*Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green);
        Debug.Log(hit.ToString());*/
        //Debug.Log(hit.collider);
        if (hit.collider != null)
        {
            hasJumped = false;
            return;
        }
        else
        {
            hasJumped = true;
            state = PlayerState.Jump;
            
        }
    }
    

    bool IsAnimationRunning(string animationName)
    {
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(animationName))
        {
            if (currentStateInfo.normalizedTime < 0.95f)
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator SwitchToLightAfterAnimation()
    {
        ResetGravity();

        state = PlayerState.Disappear;
        animator.Play(state.ToString());
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);

        transform.position = lightObj.transform.position + new Vector3(-0.2f, 0, 0);
        lightScript.enabled = true;
        this.enabled = false;
    }

    
        void ResetGravity()
    {
        shadowRb.gravityScale = 1;
        shadowRb.linearVelocity = Vector2.zero;
        shadowRb.angularVelocity = 0;
    }
}