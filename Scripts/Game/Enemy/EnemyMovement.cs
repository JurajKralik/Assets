using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool _aware { get; private set; }

    public Vector2 _directionToPlayer {  get; private set; }

    [SerializeField]
    private float _playerAwarenessDistance;
    private Transform _player;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _rotationSpeed;
    private Rigidbody2D _rigidbody;
    private Vector2 _targetDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        _directionToPlayer = enemyToPlayerVector.normalized;

        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            _aware = true;
        }
        else
        {
            _aware = false;
        }
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        if (_aware)
        {
            _targetDirection = _directionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection != Vector2.zero)
        {
            // Calculate the angle to the target direction
            float targetAngle = Mathf.Atan2(_targetDirection.y, _targetDirection.x) * Mathf.Rad2Deg;

            // Smoothly rotate towards the target angle
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, _rotationSpeed * Time.deltaTime);

            // Apply the rotation
            _rigidbody.SetRotation(currentAngle);
        }
    }

    private void SetVelocity()
    {
        if (_targetDirection != Vector2.zero)
        {
            _rigidbody.linearVelocity = _targetDirection * _speed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }
    }
}
