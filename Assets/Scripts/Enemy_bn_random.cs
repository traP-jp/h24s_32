using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy_bn_random : MonoBehaviour
{
    private Rigidbody2D rb;
    private float maxLen;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float randAngle = Random.Range(0f, 2 * math.PI);
        float randLen = Random.Range(0f, maxLen);
        Vector2 move = new Vector2((float)Math.Cos(randAngle),(float)Math.Sin(randAngle));
        rb.AddForce(move*randLen,ForceMode2D.Impulse);
    }
}
