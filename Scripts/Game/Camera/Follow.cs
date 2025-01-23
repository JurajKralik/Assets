using TMPro;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform _target;
    public Vector3 _offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            return;
        }
        Vector3 newPosition = new Vector3(_target.transform.position.x, _target.transform.position.y, _target.transform.position.z);
        newPosition += _offset;
        transform.position = newPosition;
    }
}
