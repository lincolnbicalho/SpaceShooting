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
    void Start()
    {
        _laserCountdown = Random.Range(2f, 5f);
        _animator = GetComponent<AudioSource>();
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
        if (other.tag == "Laser" && !_isSelfDestroying)
        {
            var parent = other.transform.parent;
            Destroy(other.gameObject);
            if (parent?.name != null && parent.name.Contains("Enemy"))
                return;
            else
                SelfDestroy();
        }
        else if (other.tag == "Player" && !_isSelfDestroying)
        {
            SelfDestroy();
        }
    }

    private void SelfDestroy()
    {
        _isSelfDestroying = true;
        gameObject.GetComponent<Animator>().SetTrigger("OnExplosion");
        Destroy(gameObject, 2.35f);
        _animator.Play();
    }


    public bool IsSelfDestroying()
    {
        return _isSelfDestroying;
    }
}
