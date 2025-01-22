using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    private Rigidbody2D _rigidbody;
    public float _force;
    private float _lifeTime;
    public float _lifeTimeMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lifeTime = 0;
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _mousePos = _mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = _mousePos - transform.position;
        Vector3 rotation = transform.position - _mousePos;
        _rigidbody.linearVelocity = new Vector2(direction.x, direction.y).normalized * _force;
        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        _lifeTime += Time.deltaTime;
        if (_lifeTime > _lifeTimeMax )
        {
            Destroy(gameObject);
        }
    }
}
