using System;
using System.Collections.Generic;
using Unity.Hierarchy;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private float _currentSpawnTime;
    public float _spawnTime;
    private Transform _player;
    public GameObject _enemy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = FindFirstObjectByType<PlayerMovement>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        _currentSpawnTime += Time.deltaTime;
        if (_currentSpawnTime > _spawnTime)
        {
            _currentSpawnTime = 0;
            GameObject spawner = GetRandomSpawner();
            if (spawner != null)
            {
                Instantiate(_enemy, spawner.transform.position, Quaternion.identity);
            }
        }
    }

    private GameObject GetRandomSpawner()
    {
        int randomIndex = Random.Range(0, 30);
        int currentIndex = 0;
        foreach (Transform child in transform)
        {
            if (currentIndex < randomIndex)
            {
                currentIndex++;
                continue;
            }
            else if (currentIndex > randomIndex)
            {
                return GetRandomSpawner();
            }
            Vector2 spawnerToPlayerVector = _player.position - child.gameObject.transform.position;
            float magnitude = spawnerToPlayerVector.magnitude;
            if (magnitude < 40 && magnitude > 4)
            {
                return child.gameObject;
            }
            currentIndex++;
        }
        return GetRandomSpawner();
        
    }
}
