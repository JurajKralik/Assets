using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2 _movementDirection;
    private Camera _mainCam;
    private Vector3 _mousePos;
    [SerializeField]
    private float _movementSpeed;
    public bool _canFire;
    private float _timer;
    public float _fireRate;
    public GameObject _bullet;
    public Transform _bulletTransform;
    public int _health;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health = 100;
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        _rigidbody.linearVelocity = _movementDirection * _movementSpeed;

        // Rotation
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = _mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Shooting
        if (!_canFire)
        {
            _timer += Time.deltaTime;
            if (_timer > _fireRate)
            {
                _canFire = true;
                _timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && _canFire)
        {
            _canFire = false;
            Instantiate(_bullet, _bulletTransform.position, Quaternion.identity);
        }

        // Health
        TextMesh health = GameObject.FindGameObjectWithTag("Health").GetComponent<TextMesh>();
        health.text = _health.ToString();
        if (_health <= 0)
        {
            Destroy(health);
            Destroy(gameObject);
        }
    }
    private void OnMove(InputValue inputValue)
    {
        _movementDirection = inputValue.Get<Vector2>();
    }
}