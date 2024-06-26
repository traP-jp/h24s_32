using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Homing : MonoBehaviour
{
    public bool isHoming = true;
    // ホーミングする時間
    public float homingTime = 1.5f;
    // ホーミング強度
    public float homingPower = 0.0f;
    // ホーミング強度の上がり幅
    public float homingPowerIncrease = 0.00005f;
    private float speed;
    private Rigidbody2D _rb;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _oisu__;
    private float time;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Shot>().speed;
        _rb = GetComponent<Rigidbody2D>();
        _audio = GameObject.FindGameObjectWithTag("SEController").GetComponent<AudioSource>();
        _audio.PlayOneShot(_oisu__);
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 通常時は直進
        _rb.velocity = transform.up * speed;
        // Searchで見つけた敵に向かって移動する
        if (enemy == null)
        {
            enemy = SearchEnemy();
        }
        if (isHoming && enemy != null)
        {
            Vector2 target = enemy.GetComponent<Transform>().position;
            Vector2 idealDirection = target - (Vector2)transform.position;
            Vector2 currentDirection = _rb.velocity;
            Vector2 direction = currentDirection.normalized + idealDirection.normalized * homingPower;
            // ホーミング強度に応じてx軸方向の速度を変更する
            _rb.velocity = direction.normalized * speed;
            // ホーミング角度に応じて回転させる
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
        }
        if (time > homingTime)
        {
            isHoming = false;
        }
        homingPower += homingPowerIncrease;
        time += Time.deltaTime;
    }

    // 一番近くの敵を探す
    private GameObject SearchEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject nearestEnemy = null;
        float minDistance = float.MaxValue;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
