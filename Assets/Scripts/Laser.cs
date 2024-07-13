using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _moveSpeed = 6f;
    private bool _isEnemyLaser = false;
    private void Update()
    {
        Move();
        if (transform.position.y > 7.3 && !_isEnemyLaser)
            Destroy(gameObject);
        else if(transform.position.y < -7.3 && _isEnemyLaser)
            Destroy(gameObject);
    }

    void Move()
    {
        var dicection = _isEnemyLaser ? Vector3.down : Vector3.up;
        transform.Translate(dicection * _moveSpeed * Time.deltaTime);
    }

    public void SetEnemyBehavior()
    {
        _isEnemyLaser = true;
    }
}
