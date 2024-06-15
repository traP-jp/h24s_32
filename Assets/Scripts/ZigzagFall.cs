using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigzagFall : MonoBehaviour
{
    private float initialXPosition;
    public float swingRange = 0.3f;
    public float swingSpeed = 3f;
    public float speed;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        initialXPosition = transform.position.x;
       
    }

    private void Update()
    { 
        // オブジェクトが横にゆらゆら揺れるアニメーション
        float newXPosition = initialXPosition + Mathf.PingPong(Time.time * swingSpeed, swingRange);
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        _rb.velocity = new Vector2(_rb.velocity.x, speed);
    }
}
