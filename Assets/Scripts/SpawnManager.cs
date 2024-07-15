using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private List<GameObject> _powerups;
    private float _spawnEnemyTime;
    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player is null)
            Debug.LogError("Player is NULL in Enemy.cs", _player);
        InvokeRepeating(nameof(SpawnPowerup), 4f, 7f);
    }
    void Update()
    {
        if (Time.time > _spawnEnemyTime && _player.IsAlive)
        {
            SpawnEnemy();
            _spawnEnemyTime = Time.time + Random.Range(1, 5);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-4.94f, 4.94f), 8f), Quaternion.identity, _enemyContainer.transform);
    }

    void SpawnPowerup()
    {
        if (_player.IsAlive)
            Instantiate(_powerups[Random.Range(0, 3)], new Vector3(Random.Range(-4.94f, 4.94f), 8f), Quaternion.identity);
        else
            CancelInvoke("SpawnPowerup");

    }
}
