using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_bn_Homing : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject player;
    private float time;
    public float freezTime;
    public float enemySpeed;
    public int homingTimes;
    public int maxHomingTimes;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.drag = 0;
        time = 0f;
        homingTimes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > freezTime)
        {
            homingTimes++;
            if (homingTimes < maxHomingTimes)
            {
                //一旦停止して打ち出す
                rb.velocity = new Vector2(0, 0);
                //freezTimeを超えるとenemyからplayerに向かって打ち出されるイメージ
                Vector2 thisPosition = (Vector2)this.transform.position;
                Vector2 otherPosition = (Vector2)player.transform.position;
                Vector2 difVector = otherPosition - thisPosition;
                Vector2 difVectorE = difVector.normalized;
                rb.AddForce(difVectorE * enemySpeed, ForceMode2D.Impulse);
                time = 0f;
            }

        }
    }
}
