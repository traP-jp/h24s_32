using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Straight : MonoBehaviour
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
        _rb.velocity = transform.up * speed;
    }
}
