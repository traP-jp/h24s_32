using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public float targetVelocity; 
    Rigidbody2D rb;
 private void FixedUpdate(){
    const float power = 20;
    rb.AddForce(Vector3.down * ((targetVelocity - rb.velocity.x) * power), ForceMode2D.Force);
}

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
