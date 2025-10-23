using System.Collections;
using UnityEngine;


public class Shadow : MonoBehaviour
{
    //Basic movement stats
    public float MoveSpeed = 5.0f;
    [SerializeField] private float DashSpeed = 20.0f;
    [SerializeField] private float DashDistance = 5f;
    public float JumpForce = 1f;   
    private bool _hasJumped = false;

    //Animator and stat to control the animations
    public Animator Animator;
    public PlayerState State;
    private bool _stopAnimations = false;

    //Sprite renderer to control the flip x
    private SpriteRenderer _spriteRenderer;

    //Light refrences 
    public GameObject LightObj;
    private Light _lightScript;

    public Rigidbody2D ShadowRb;

    //Raycast setting
    public Vector2 BoxSize = new Vector2(0.1f, 0.2f);
    public LayerMask ShadowGround;
    public float RayLength;

    private float _horizontal;

    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        _lightScript = LightObj.GetComponentInChildren<Light>();
        
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

            if (Input.GetKeyDown(KeyCode.G))
            {
                ToggleMode();
            }

            Jump();

            _horizontal = Input.GetAxis("Horizontal");

            if(!_stopAnimations)
                Animator.Play(State.ToString());
        }
    }

  

    private void FixedUpdate()
    {
        Move();
    }

    private IEnumerator DashingAction(float XDirection = 0, float YDirection = 0)
    {
        // Directions can be 1 or -1
        if (XDirection == 0 && YDirection == 0)
            yield break;

        Vector2 dashDir = new Vector2(XDirection, YDirection).normalized;
        Vector3 targetPos = transform.position + (Vector3)(dashDir * DashDistance);

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                DashSpeed * Time.deltaTime
            );

            yield return null; 
        }

        transform.position = targetPos; 
    }
    private void Move()
    {
        if (_horizontal != 0)
        {
            State = PlayerState.Walk;
            transform.position += Vector3.right * (_horizontal * MoveSpeed * Time.deltaTime);

            _spriteRenderer.flipX = _horizontal > 0;
        }
        else
        {
            State = PlayerState.Idle;
        }
    }
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_hasJumped)
        {
            _hasJumped = true;
            ShadowRb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
        }
    }
    public void ToggleMode()
    {
        if (!_lightScript.enabled)
            StartCoroutine(SwitchToLightAfterAnimation());
    }

    public void PlayShadowFootSteps(AudioClip clip)
    {
         if (!_lightScript.enabled)
        {
            AudioManager.Instance.PlaySFXCustom(clip, 1.0f);
        }
        
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, BoxSize, 0f, Vector2.down, RayLength, ShadowGround);
        if (hit.collider != null)
        {
            _hasJumped = false;
            return;
        }
        else
        {
            _hasJumped = true;
            State = PlayerState.Jump;
            
        }
    }

    bool IsAnimationRunning(string animationName)
    {
        AnimatorStateInfo currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
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

        _stopAnimations = true;
        State = PlayerState.Disappear;
        Animator.Play(State.ToString());

        AnimatorStateInfo currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);
        
        transform.position = LightObj.transform.position + new Vector3(-0.2f, 0, 0);

        _stopAnimations = false;
        _lightScript.enabled = true;
        this.enabled = false;
        
    }

    
        void ResetGravity()
    {
        ShadowRb.gravityScale = 1;
        ShadowRb.linearVelocity = Vector2.zero;
        ShadowRb.angularVelocity = 0;
        _horizontal = 0;
    } //Reset garavity so it wont accmulat while not in shadow mode
}