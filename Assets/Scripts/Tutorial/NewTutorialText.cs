using UnityEngine;

public class NewTutorialText : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
           _animator = GetComponent<Animator>();
    }

    bool IsAnimationRunning(string animationName)
    {
        AnimatorStateInfo currentStateInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(animationName))
        {
            if (currentStateInfo.normalizedTime < 0.95f)
            {
                return true;
            }
        }
        return false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerShadow") && !IsAnimationRunning("FadeIn"))
        {
            _animator.SetTrigger("ShowText");
        }
    }
}
