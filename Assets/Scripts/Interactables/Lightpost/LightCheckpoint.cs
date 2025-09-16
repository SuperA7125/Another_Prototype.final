using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCheckpoint : MonoBehaviour
{
    private Animator _animator;

    public Light2D Light2D;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetLastCheckpoint(transform.position);
            _animator.Play("On");
            Light2D.enabled = true;
        }
    }


}
