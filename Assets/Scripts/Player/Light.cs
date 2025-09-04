using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public class Light : MonoBehaviour
{
    public float speed = 5.0f;

    public float jumpForce = 1f;

    public bool hasJumped = false;

    bool isDead = false;
    public Animator animator;

    public Animator shadowAnimator;

    public PlayerState state;

    private SpriteRenderer spriteRenderer;

    private SpriteRenderer shadowRenderer;

    private float shadowOffsetX;

    public GameObject shadowObj;

    public GameObject lightObj;

    Shadow shadowScript;

    public bool shadowNeedsActivition = true;

    public Rigidbody2D lightRb;

    public LayerMask lightGround;

    public float rayLength;

    private float horizontal;

    

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        shadowScript = shadowObj.GetComponentInChildren<Shadow>();
        shadowRenderer = shadowObj.GetComponent<SpriteRenderer>();
        shadowScript.enabled = false;

    }

    void Update()
    {

        if (!shadowNeedsActivition)
        {
            StartCoroutine(WaitForShadowToAppear());
        }
        GroundCheck();

        SetShadowPos();

        
        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Key Down!");
            ToggleMode();
        }
        if (!IsAnimationRunning("Disappear"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !hasJumped)
            {
                hasJumped = true;
                lightRb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
        horizontal = Input.GetAxis("Horizontal");

        if (!IsAnimationRunning("Disappear") && !isDead) 
        {
            animator.Play(state.ToString());
            if (!IsShadowAnimationRunning("Appear"))
            {
                shadowAnimator.Play(state.ToString());
            }
        }
    }

    private void FixedUpdate()
    {

        if (!IsAnimationRunning("Disappear"))
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
    }
    void ToggleMode()
    {
        if (!shadowScript.enabled)
        {
            state = PlayerState.Idle;
            animator.Play(state.ToString());
            shadowNeedsActivition = false;
            shadowScript.enabled = true;
            this.enabled = false;
        }
    }

    public void StartDeathAndRespawn()
    {
        StartCoroutine(DeathAndRespawn());
    }
    void SetShadowPos()
    {
        shadowOffsetX = spriteRenderer.flipX ? 0.05f : -0.05f;
        shadowObj.transform.position = new Vector3(transform.position.x - shadowOffsetX, transform.position.y, 0);

        shadowRenderer.flipX = spriteRenderer.flipX;
        
    }

    public void PlayLightFootSteps(AudioClip clip)
    {
       

        AudioManager.Instance.PlaySFXCustom(clip ,1.0f);
    }

    
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength, lightGround);

        /*Debug.DrawRay(transform.position, Vector2.down * rayLength, Color.green,14444f);
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

    bool IsShadowAnimationRunning(string animationName)
    {
        AnimatorStateInfo currentStateInfo = shadowAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(animationName))
        {
            if (currentStateInfo.normalizedTime < 0.95f)
            {
                return true;
            }
        }
        return false;
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

    IEnumerator WaitForShadowToAppear()
    {
        shadowOffsetX = spriteRenderer.flipX ? 0.2f : -0.2f;
        shadowObj.transform.position = new Vector3(transform.position.x - shadowOffsetX, transform.position.y, 0);

        shadowRenderer.flipX = spriteRenderer.flipX;
        shadowAnimator.Play("Appear");
        AnimatorStateInfo currentStateInfo = shadowAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length -0.1f);
        shadowNeedsActivition = true;
    }

    IEnumerator DeathAndRespawn()
    {
        isDead = true;
        animator.Play("Disappear");
        shadowAnimator.Play("Disappear");
        
        AnimatorStateInfo currentStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);

        GameManager.Instance.RespawnPlayer(lightObj);

        yield return new WaitForSeconds(0.06f);

        isDead = false;
    }
}
