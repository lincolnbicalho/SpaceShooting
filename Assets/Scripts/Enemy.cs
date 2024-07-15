using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;
    private float _moveSpeed = 2f;
    private float _laserCountdown;
    private bool _isSelfDestroying = false;
    private AudioSource _animator;
    private GameManager _gameManager;
    void Start()
    {
        _laserCountdown = Random.Range(2f, 5f);
        _animator = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (_gameManager is null)
            Debug.LogError("GameManager is NULL in Enemy.cs", _gameManager);
    }

    void Update()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(-9.54f, 9.54f), 8f);
        }

        if (_laserCountdown < Time.time && _isSelfDestroying == false)
        {
            var _laser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -1f), Quaternion.identity);
            _laser.GetComponent<Laser>().SetEnemyBehavior();
            _laser.transform.parent = transform;
            _laserCountdown = Time.time + Random.Range(2f, 5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            var parent = other.transform.parent;
            Destroy(other.gameObject);
            if (parent?.name != null && parent.name.Contains("Enemy"))
                return;
            else
                SelfDestroy();
        }
        else if (other.tag == "Player")
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        _gameManager.SetScore(10);
        Destroy(gameObject.GetComponent<Collider2D>());
        gameObject.GetComponent<Animator>().SetTrigger("OnExplosion");
        Destroy(gameObject, 2.35f);
        _animator.Play();
        _isSelfDestroying = true;
    }


    public bool IsSelfDestroying()
    {
        return _isSelfDestroying;
    }
}
