using TMPro;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform _target;
    public Vector3 _offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            FindPlayer();
            return;
        }
        Vector3 newPosition = new Vector3(_target.position.x, _target.position.y, _target.position.z);
        newPosition += _offset;
        transform.position = newPosition;
    }
    private void FindPlayer()
    {
        PlayerMovement target = FindFirstObjectByType<PlayerMovement>();
        if (target != null)
        {
            _target = target.transform;
        }
        else
        {
            _target = null;
        }
        
    }
}
