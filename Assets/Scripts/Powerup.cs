using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.down * 3 * Time.deltaTime);
        if (transform.position.y < -5.6)
            Destroy(gameObject);
    }
}
