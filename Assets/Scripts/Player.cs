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
    [SerializeField]
    private GameObject _playerShield;
    private bool _isShieldActivate = false;
    private bool _isTripleshotActive = false;
    private float _moveSpeed = 3f;
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

        var moveVector = new Vector3(xInput, yInput, 0) * _moveSpeed * Time.deltaTime;
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
            if (_isTripleshotActive)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1f), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(-0.621f, -0.13f), Quaternion.identity);
                Instantiate(_laserPrefab, transform.position + new Vector3(0.621f, -0.13f), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1f), Quaternion.identity);
            }
            
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
            var enemy = other.GetComponentInParent<Enemy>();
            if (enemy is not null)
            {
                Damage();
                Destroy(other.gameObject);
            }
        }
        else if (other.tag == "Enemy")
        {
            Damage();
            //if (!other.GetComponent<Enemy>().IsSelfDestroying())  
        }
        else if (other.tag == "Powerup")
        {
            PowerupPlayer(other.name);
            Destroy(other.gameObject);
        }

    }

    private void PowerupPlayer(string name)
    {
        if (name.Contains("Tripleshot"))
        {
            StartCoroutine(ActivateTripleshot());
        }
        else if (name.Contains("Shield"))
        {
            StartCoroutine(ActivateShield());
        }
        else if (name.Contains("Speed"))
        {
            StartCoroutine(SpeedPlayer());
        }
    }
    IEnumerator SpeedPlayer()
    {
        _moveSpeed = 6f;
        yield return new WaitForSeconds(5);
        _moveSpeed = 3f;
    }

    IEnumerator ActivateShield()
    {
        _playerShield.SetActive(true);
        _isShieldActivate = true;
        yield return new WaitForSeconds(7);
        _playerShield.SetActive(false);
        _isShieldActivate = false;
    }

    IEnumerator ActivateTripleshot()
    {
        _isTripleshotActive = true;
        yield return new WaitForSeconds(5);
        _isTripleshotActive = false;
    }
    public void Damage()
    {
        if (_isShieldActivate)
            return;
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
            GameObject.Find("GameManager")?.GetComponent<GameManager>().GameOver();
        }
    }
    public bool IsAlive { get => (_lives > 0) ? true : false;  }  
    public int Lives { get { return _lives; } }
}

