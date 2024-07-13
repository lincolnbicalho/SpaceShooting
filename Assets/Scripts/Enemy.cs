using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private float _moveSpeed = 2f;
    private float _laserCountdown;
    void Start()
    {
        _laserCountdown = Random.Range(0.5f, 1.5f);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if(transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-9.54f, 9.54f), 8f);
        }

        if(_laserCountdown < Time.time)
        {
            var _laser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -0.98f), Quaternion.identity);
            _laser.GetComponent<Laser>().SetEnemyBehavior();
            _laserCountdown = Time.time + Random.Range(1.5f, 4f);
        }
    }
}
