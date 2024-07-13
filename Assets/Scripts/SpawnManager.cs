using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _enemyPrefab;
    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 4f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        Instantiate(_enemyPrefab, new Vector3(Random.Range(-4.94f, 4.94f), 8f), Quaternion.identity, _enemyContainer.transform);
    }

}
