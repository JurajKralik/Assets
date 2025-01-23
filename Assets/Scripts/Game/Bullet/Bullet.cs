using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _mousePos;
    private Camera _mainCam;
    private Rigidbody2D _rigidbody;
    private CircleCollider2D _collider;
    public float _force;
    private float _lifeTime;
    public float _lifeTimeMax;
    private AudioSource _audio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _lifeTime = 0;
        _mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<CircleCollider2D>();
        _audio = GetComponent<AudioSource>();
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
            Destroy(_rigidbody);
            Destroy(_collider);
        }
        if (_rigidbody == null)
        {
            if (!_audio.isPlaying)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;
        if ( hitObject.name.Contains("Enemy"))
        {
            Destroy(hitObject);
        }
        Destroy(_rigidbody);
        Destroy(_collider );
    }
}
