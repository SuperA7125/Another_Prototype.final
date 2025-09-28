using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Vector3 _startingPos;
    private bool _movingToEnd = true;

    public Vector3 EndPos;
    public float Speed = 2f;
    public float Tolerance = 0.01f;

    private void Start()
    {
        _startingPos = transform.position;
    }

    private void Update()
    {
        TogglePlatforms();
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