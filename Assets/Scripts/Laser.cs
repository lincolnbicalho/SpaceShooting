using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float _moveSpeed = 6f;
    private void Update()
    {
        transform.Translate(Vector3.up * _moveSpeed * Time.deltaTime);
        if(transform.position.y > 7.3)
            Destroy(gameObject);
    }
}
