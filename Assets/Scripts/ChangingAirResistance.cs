using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ChangingAirResistance : MonoBehaviour
{
    public Rigidbody2D rb;
    private float timer = 0;
    public float Appeartime;
    public float drag;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
   void UpwardForce()
    {
        rb.drag = drag;
    }
 
    void Update()
    {   
        timer += Time.deltaTime;
         if (timer > Appeartime)   
         {
           UpwardForce();
         }
    }
}
