using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    public float coolTimeMax = 1;
    float coolTime = -100;//これが正だとショットを撃てない
    public float moveSpeed = 5;
    public float leftPosLimit = -1.5f;//左方向移動の上限
    public float rightPosLimit = 4.6f;//右方向移動の上限
    public float moveFreezeTimeLimit = 0.3f;
    public float jumpPower = 15;
    public float gravityForce_Rise = 30;
    public float gravityForce_Fall = 10;
    [SerializeField] float groundPos = -3.76f;
    float currentJumpSpeed = -10000;
    float moveFreezeTime = -100; //これが正だと動けない
    [SerializeField] Vector3 ShotSpawnPos = new Vector3(0, 0.5f, 0);
    [SerializeField] GameObject normalShot;
    Rigidbody2D rb;
    bool RightPushed = false;
    bool LeftPushed = false;
    bool UpPushed = false;
    bool SpacePushed = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PushCheck();
        if (moveFreezeTime < 0)
        {
            if (LeftPushed && transform.position.x > leftPosLimit)
            {
                rb.position += new Vector2(-moveSpeed * Time.deltaTime, 0);
            }
            else if (RightPushed && transform.position.x < rightPosLimit)
            {
                rb.position += new Vector2(moveSpeed * Time.deltaTime, 0);
            }
        }
        else
        {
            moveFreezeTime -= Time.deltaTime;
        }
        if (coolTime < 0 && SpacePushed)
        {
            Vector3 shotBasePos = transform.position;
            Instantiate(normalShot, shotBasePos += ShotSpawnPos, quaternion.identity);
            moveFreezeTime = moveFreezeTimeLimit;
            coolTime = coolTimeMax;
        }
        else if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        if (UpPushed && transform.position.y < -3.7f)
        {
            currentJumpSpeed = jumpPower;
        }
        if (currentJumpSpeed > -5000)
        {
            if (currentJumpSpeed > 0)
            {
                currentJumpSpeed -= gravityForce_Rise * Time.deltaTime;
            }
            else
            {
                currentJumpSpeed -= gravityForce_Fall * Time.deltaTime;
            }
            rb.position += new Vector2(0, currentJumpSpeed * Time.deltaTime);
            if (rb.position.y < groundPos)
            {
                rb.position = new Vector2(rb.position.x, groundPos);
                currentJumpSpeed = -10000;
            }
        }
        PushReset();
    }
    void PushCheck()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            LeftPushed = true;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RightPushed = true;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            UpPushed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SpacePushed = true;
        }
    }
    void PushReset()
    {
        RightPushed = false;
        LeftPushed = false;
        UpPushed = false;
        SpacePushed = false;
    }
}
