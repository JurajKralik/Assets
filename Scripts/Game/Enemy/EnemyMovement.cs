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
    public float _attackSpeed;
    private float _reload;
    private bool _canAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _aware = true;
        if (!_player)
        {
            return;
        }
        UpdateTargetDirection();
        RotateTowardsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player)
        {
            return;
        }
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        _directionToPlayer = enemyToPlayerVector.normalized;
        float magnitude = enemyToPlayerVector.magnitude;

        if (magnitude <= _playerAwarenessDistance)
        {
            _aware = true;
        }
        else
        {
            _aware = false;
        }
        Attack(magnitude);
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void Attack(float magnitude)
    {
        if (!_canAttack)
        {
            _reload += Time.deltaTime;
            if (_reload > _attackSpeed)
            {
                _reload = 0;
                _canAttack = true;
            }
            return;
        }

        if (magnitude < 1)
        {
            PlayerMovement player = FindFirstObjectByType<PlayerMovement>();
            if (player != null)
            {
                player._health -= 10;
                _canAttack = false;
            }
        }
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
