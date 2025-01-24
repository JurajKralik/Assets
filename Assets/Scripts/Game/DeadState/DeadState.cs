using UnityEngine;

public class DeadState : MonoBehaviour
{
    public GameObject _player;
    public GameObject _health;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 spawnPosition = new Vector3(0, 0, 0);
            Instantiate(_player, spawnPosition, Quaternion.identity);
            Instantiate(_health, spawnPosition, Quaternion.identity);
            _health.tag = "Health";
            Destroy(gameObject);
        }
    }
}
