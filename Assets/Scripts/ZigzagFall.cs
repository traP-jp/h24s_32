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
        _rb.MovePosition(new Vector2(newXPosition, transform.position.y + speed*Time.deltaTime));
    }
}
