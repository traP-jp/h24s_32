using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_bn_path : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject pathObject;
    public GameObject path;
    private float time;
    private Vector2 difVectorE = new Vector2(0, 0);
    private Vector2 difVector = new Vector2(0, 0);
    private Vector2 thisPosition = new Vector2(0, 0);
    private Vector2 otherPosition = new Vector2(0, 0);
    private bool isAct;
    private bool isPathMade;
    private bool isMove;
    private bool isPathDeled;
    public float freezTime;
    public float actStartTime;
    public float moveStartTime;
    private float pathDisplayTime;
    public float enemySpeed;
    public float firstPathSize;
    public float enemyAirResistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        rb.gravityScale = 0;
        rb.drag = enemyAirResistance;
        time = 0f;
        isAct = false;
        isPathMade = false;
        isMove = false;
        isPathDeled = false;
        pathDisplayTime = moveStartTime - actStartTime;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //freezTimeを超えるとenemyからplayerに向かって打ち出されるイメージ
        if (time > freezTime)
        {
            if (isAct == false)
            {
                //差分のベクトルを定める
                thisPosition = (Vector2)this.transform.position;
                otherPosition = (Vector2)player.transform.position;
                difVector = otherPosition - thisPosition;
                difVectorE = difVector.normalized;
                isAct = true;
            }
        }
        if (time > actStartTime)
        {
            //pathを表示
            if (isPathMade == false)
            {
                pathObject = Instantiate(path);
                pathObject.transform.position = thisPosition + difVector * 0.5f;
                pathObject.transform.localScale = new Vector2(difVector.magnitude, firstPathSize);
                if (difVectorE != Vector2.zero)
                {
                    // ベクトルの角度を計算する
                    float angle = Mathf.Atan2(difVectorE.y, difVectorE.x) * Mathf.Rad2Deg;

                    // Z軸周りの回転を設定
                    pathObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                }
                isPathMade = true;
            }
            else
            {
                if (isPathDeled == false)
                {
                    //timeでいい感じにサイズを変える
                    pathObject.transform.localScale = new Vector2(difVector.magnitude, firstPathSize * ((moveStartTime - time) / pathDisplayTime));
                    if (moveStartTime - time < 0)
                    {
                        Destroy(pathObject);
                        isPathDeled = true;
                    }
                }
            }
        }
        if (time > moveStartTime)
        {
            if (isMove == false)
            {
                rb.AddForce(difVectorE * enemySpeed, ForceMode2D.Impulse);
                isMove = true;
            }
        }
        if (isMove == true)
        {
            Vector2 thisPosition_ = (Vector2)this.transform.position;
            Vector2 otherPosition_ = (Vector2)player.transform.position;
            Vector2 difVector_ = otherPosition_ - thisPosition_;
            if (difVector_.magnitude > difVector.magnitude * 1.5f || Math.Round(rb.velocity.magnitude,1) == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        Destroy(pathObject);
    }
}