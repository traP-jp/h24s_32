using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shot_Kinano : MonoBehaviour
{
    public float shotInterval = 1.0f;
    public GameObject shotPrefab;
    // プレイヤーとの距離
    public float distanceBetweenPlayer = 1.0f;
    private float time = 0.0f;
    private float speed;
    private Rigidbody2D _rb;
    private GameObject _player;
    private AudioSource _kinanoShotAudio;
    [SerializeField]
    private AudioClip _kinanoShotClip;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Shot>().speed;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _kinanoShotAudio = GameObject.FindGameObjectWithTag("SEController").GetComponent<AudioSource>();
        _kinanoShotAudio.PlayOneShot(_kinanoShotClip);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shot();
        time += Time.deltaTime;
    }

    private void Move()
    {
        if (_player != null)
        {
            // プレイヤーに近づいていく
            if (Vector2.Distance(_player.transform.position, transform.position) > distanceBetweenPlayer)
            {
                Vector2 target = _player.GetComponent<Transform>().position;
                Vector2 idealDirection = target - (Vector2)transform.position;
                Vector2 currentDirection = _rb.velocity;
                Vector2 direction = currentDirection.normalized + idealDirection.normalized;
                _rb.velocity = direction.normalized * speed;
            }
            // プレイヤー周辺を周る
            else
            {
                Vector2 target = _player.GetComponent<Transform>().position;
                Vector2 targetDirection = target - (Vector2)transform.position;
                Vector2 direction = Vector2.Perpendicular(targetDirection).normalized;
                _rb.velocity = direction * speed;
            }
        }
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Shot()
    {
        if (shotPrefab != null && time > shotInterval)
        {
            Instantiate(shotPrefab, transform.position, Quaternion.identity);
            time = 0.0f;
        }
    }
}
