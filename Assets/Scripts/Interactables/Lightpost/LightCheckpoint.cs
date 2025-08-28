using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightCheckpoint : MonoBehaviour
{
    Animator animator;

    public Light2D light2D;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetLastCheckpoint(transform.position);
            animator.Play("On");
            light2D.enabled = true;
            Debug.Log("Checkpoint made!");
        }
    }


}
