using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Shot : MonoBehaviour
{
    public float damage;
    public float speed = 5.0f;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 常に上に移動する
        _rb.velocity = Vector2.up * speed;
    }
}
