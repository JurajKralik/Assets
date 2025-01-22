using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform _target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(_target.transform.position.x, _target.transform.position.y, _target.transform.position.z - 10);
    }
}
