using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _powerups;
    private float _spawnEnemyTime;
    void Start()
    {
        SpawnEnemy();
        InvokeRepeating(nameof(SpawnPowerup), 4f, 7f);
    }
    void Update()
    {
        if(Time.time > _spawnEnemyTime)
        {
            SpawnEnemy();
            _spawnEnemyTime = Time.time + Random.Range(1.5f, 5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-4.94f, 4.94f), 8f), Quaternion.identity, _enemyContainer.transform);
    }

    void SpawnPowerup()
    {

        Instantiate(_powerups[Random.Range(0, 3)], new Vector3(Random.Range(-4.94f, 4.94f), 8f), Quaternion.identity);
    }

}
