using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Vector3 _startingPos;
    private bool _movingToEnd = true;

    public Vector3 EndPos;
    public float Speed = 2f;
    public float Tolerance = 0.01f;

    public Camera Camera;

    [SerializeField]List<string> _possibleTags;

    Dictionary<Transform,Transform> CharacterParentPair = new();
    private void Start()
    {
        Camera = FindAnyObjectByType<Camera>();
        _startingPos = transform.position;
    }

    private void Update()
    {
        TogglePlatforms();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        foreach (string tag in _possibleTags)
        {
            if (other.collider.CompareTag(tag))
            {
                Camera?.transform.SetParent(transform, true);
                CharacterParentPair[other.collider.transform] = other.collider.transform.parent;
                other.collider.transform.SetParent(transform, true);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
        foreach (string tag in _possibleTags)
        {
            if (other.collider.CompareTag(tag))
            {
                Camera?.transform.SetParent(null, true);
                other.collider.transform.SetParent(CharacterParentPair[other.collider.transform], true);
            }
        }
    }
    void TogglePlatforms()
    {
        Vector3 target = _movingToEnd ? EndPos : _startingPos;

        
        transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) <= Tolerance)
        {
            _movingToEnd = !_movingToEnd;
        }
    }
}