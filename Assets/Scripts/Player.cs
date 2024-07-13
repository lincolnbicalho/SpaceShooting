using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private float moveSpeed = 3f;
    private float gunHeat;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        ShootLaser();
    }


    void PlayerMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        var moveVector = new Vector3(xInput, yInput, 0) * moveSpeed * Time.deltaTime;
        var newPosition = transform.position + moveVector;

        newPosition.x = Mathf.Clamp(newPosition.x, -9.9f, 9.9f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4.0f, 4.0f);

        transform.Translate(newPosition - transform.position);
    }

    void ShootLaser()
    {
        gunHeat -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && gunHeat <= 0 )
        {
            gunHeat = 0.5f;
            Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0.98f), Quaternion.identity);
        }
            
    }
}
