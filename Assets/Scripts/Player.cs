using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private GameObject _leftWingDamage;
    [SerializeField]
    private GameObject _rightWingDamage;
    private float moveSpeed = 3f;
    private float gunHeat;
    private Animator _animator;
    private bool _isTurn = false;
    void Start()
    {
        _animator = GetComponent<Animator>();
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
        if (_lives > 0)
            AnimationTurn(xInput, yInput);

        var moveVector = new Vector3(xInput, yInput, 0) * moveSpeed * Time.deltaTime;
        var newPosition = transform.position + moveVector;

        newPosition.x = Mathf.Clamp(newPosition.x, -9.9f, 9.9f);
        newPosition.y = Mathf.Clamp(newPosition.y, -4.0f, 2.0f);

        transform.Translate(newPosition - transform.position);
    }

    void ShootLaser()
    {
        gunHeat -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && gunHeat <= 0)
        {
            gunHeat = 0.3f;
            var shoot = Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1f), Quaternion.identity);
        }
    }

    void AnimationTurn(float xInput, float yInput)
    {
        if (xInput > 0f && !_isTurn)
        {
            _isTurn = true;
            _animator.SetTrigger("OnRightTurn");
        }
        else if (xInput < 0f && !_isTurn)
        {
            _isTurn = true;
            _animator.SetTrigger("OnLeftTurn");
        }
        else if (xInput == 0f && _isTurn)
        {
            _isTurn = false;
            _animator.SetTrigger("OnExit");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            var player = other.GetComponentInParent<Player>();
            if (player is null)
            {
                Damage();
                Destroy(other.gameObject);
            }
        }
        else if (other.tag == "Enemy")
        {
            if (!other.GetComponent<Enemy>().IsSelfDestroying())
                Damage();
        }
        else if (other.tag == "Powerup")
        {
            PowerupPlayer(other.name);
            Destroy(other.gameObject);
        }

    }

    private void PowerupPlayer(string name)
    {
        if (name.Contains("Tripleshot")) { }
        else if (name.Contains("Shield")) { }
        else if (name.Contains("Speed")) { }
        Debug.LogWarning(name);
    }

    public void Damage()
    {
        _lives--;
        switch (_lives)
        {
            case 2:
                _rightWingDamage.SetActive(true);
                break;
            case 1:
                _leftWingDamage.SetActive(true);
                break;
            case 0:
                var spawnExplosin = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(spawnExplosin, 2.5f);
                Destroy(gameObject, .2f);
                break;
        }
        if (_lives == 0)
        {

        }
    }
}

