using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Homing : MonoBehaviour
{
    private float speed;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<Shot>().speed;
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 通常時は直進
        _rb.velocity = transform.up * speed;
        // Searchで見つけた敵に向かって移動する
        GameObject enemy = SearchEnemy();
        if (enemy != null)
        {
            Vector2 target = enemy.GetComponent<Transform>().position;
            Vector2 direction = target - (Vector2)transform.position;
            _rb.velocity = direction.normalized * speed;
        }
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
