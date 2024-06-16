using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_bn_random : MonoBehaviour
{
    private Rigidbody2D rb;
    public float maxLen;
    private float time;
    public float freezeTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 1.21f;
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > freezeTime)
        {
            float randAngle = Random.Range(0f, 2f * math.PI);
            float randLen = Random.Range(0f, maxLen);
            Vector2 move = new Vector2((float)Math.Cos(randAngle), (float)Math.Sin(randAngle));
            rb.AddForce(move * randLen, ForceMode2D.Impulse);
            time = 0;
        }
    }
}
