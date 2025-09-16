using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
public class Light : MonoBehaviour
{
    //Basic movement stats
    public float MoveSpeed = 5.0f;
    public float JumpForce = 1f;
    private bool _hasJumped = false;

    private bool _isDead = false; //Sets if the player is dead

    //Animators to control the light animations and make sure that shdow mirrors them when it isnt active
    public Animator Animator;
    public Animator ShadowAnimator;

    public PlayerState State; //The State the Animator needs to follows

    //Refrerences to the sprite Renderes to set the flip x
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _shadowRenderer;

    private float _shadowOffsetX;//The offest that the shadow is from the light

    //Refrences for the light and shadow objects
    public GameObject ShadowObj;
    public GameObject LightObj;

    private Shadow _shadowScript;//Refrence to the shadow script to be able to turn it off but still have the Animator working

    public bool ShadowNeedsActivition = true;

    public Rigidbody2D LightRb;

    //Raycast settings
    public LayerMask LightGround;
    public float RayLength;
    private float _horizontal;

    

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
        _shadowScript = ShadowObj.GetComponentInChildren<Shadow>();
        _shadowRenderer = ShadowObj.GetComponent<SpriteRenderer>();
        _shadowScript.enabled = false;
        Animator.speed = 1f;
        ShadowAnimator.speed = 1f;

    }

    void Update()
    {
        if (_isDead) return;
        if (!ShadowNeedsActivition)
        {
            StartCoroutine(WaitForShadowToAppear());
        }

        GroundCheck();

        SetShadowPos();


        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleMode();
        }

        Jump();

        _horizontal = Input.GetAxis("Horizontal");

        PlayAnimations();
    }
    private void FixedUpdate()
    {
        if (_isDead) return;
        if (!IsAnimationRunning("Disappear"))
        {
            Move();
        }
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
        if (!IsAnimationRunning("Disappear"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_hasJumped)
            {
                _hasJumped = true;
                LightRb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void PlayAnimations()
    {
        if (!IsAnimationRunning("Disappear") && !_isDead)
        {
            Animator.Play(State.ToString());
            if (!IsShadowAnimationRunning("Appear"))
            {
                ShadowAnimator.Play(State.ToString());
            }
        }
    }
    void ToggleMode()
    {
        if (!_shadowScript.enabled)
        {
            State = PlayerState.Idle;
            Animator.Play(State.ToString());
            ShadowNeedsActivition = false;
            _shadowScript.enabled = true;
            this.enabled = false;
        }
    }


    void SetShadowPos()
    {
        _shadowOffsetX = _spriteRenderer.flipX ? 0.05f : -0.05f;
        ShadowObj.transform.position = new Vector3(transform.position.x - _shadowOffsetX, transform.position.y, 0);

        _shadowRenderer.flipX = _spriteRenderer.flipX;
        
    }

    public void PlayLightFootSteps(AudioClip clip)
    {
        AudioManager.Instance.PlaySFXCustom(clip ,1.0f);
    }
    
    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, RayLength, LightGround);

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

    bool IsShadowAnimationRunning(string animationName)
    {
        AnimatorStateInfo currentStateInfo = ShadowAnimator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(animationName))
        {
            if (currentStateInfo.normalizedTime < 0.95f)
            {
                return true;
            }
        }
        return false;
    } //Needs 2 to be able to set the animations indepandetly

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

   //Death
    public void StartDeathAndRespawn()
    {
        StartCoroutine(DeathAndRespawn());
    }

    public void StartDeath()
    {
        StartCoroutine(Death());
    }

    //Corotines

    IEnumerator DeathAndRespawn()
    {
        _isDead = true;
        Animator.Play("Disappear");
        ShadowAnimator.Play("Disappear");
        
        AnimatorStateInfo currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);

        GameManager.Instance.RespawnPlayer(LightObj);

        yield return new WaitForSeconds(0.06f);

        _isDead = false;
    }

    IEnumerator Death()
    {
        _isDead = true;
        Animator.speed = 0.3f;
        ShadowAnimator.speed = 0.3f;
        Animator.Play("Disappear");
        ShadowAnimator.Play("Disappear");


        AnimatorStateInfo currentStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);
    }

    IEnumerator WaitForShadowToAppear()
    {
        _shadowOffsetX = _spriteRenderer.flipX ? 0.2f : -0.2f;
        ShadowObj.transform.position = new Vector3(transform.position.x - _shadowOffsetX, transform.position.y, 0);

        _shadowRenderer.flipX = _spriteRenderer.flipX;
        ShadowAnimator.Play("Appear");
        AnimatorStateInfo currentStateInfo = ShadowAnimator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currentStateInfo.length - 0.1f);
        ShadowNeedsActivition = true;
    }
}
