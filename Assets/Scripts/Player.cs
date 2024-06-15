using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public int maxHP = 10;
    public int currentHP;
    public float currentHP_Damage_Tween;
    public float coolTimeMax = 1;
    public float coolTime = -100;//これが正だとショットを撃てない
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
    float damageHPMoveTimeMAX = 1;
    float damageHPMoveTime = 100000;
    float damageDecreaseTime = 0.5f; //Tweenに突っ込む用
    [SerializeField] Vector3 ShotSpawnPos = new Vector3(0, 0.5f, 0);
    [SerializeField] GameObject normalShot;
    Rigidbody2D rb;
    SpriteRenderer playerRenderer;
    bool RightPushed = false;
    bool LeftPushed = false;
    bool UpPushed = false;
    bool SpacePushed = false;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        currentHP_Damage_Tween = maxHP;
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();
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
                playerRenderer.flipX = false;
            }
            else if (RightPushed && transform.position.x < rightPosLimit)
            {
                rb.position += new Vector2(moveSpeed * Time.deltaTime, 0);
                playerRenderer.flipX = true;
            }
        }
        else
        {
            moveFreezeTime -= Time.deltaTime;
        }
        if (coolTime < 0 && SpacePushed)
        {
            Vector3 shotBasePos = transform.position;
            GameObject go = Instantiate(normalShot);
            go.transform.position = shotBasePos += ShotSpawnPos;
            moveFreezeTime = moveFreezeTimeLimit;
            coolTime = coolTimeMax;
        }
        else if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        if (UpPushed && transform.position.y < groundPos + 0.1f)
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
        if (damageHPMoveTime < 5000)
        {
            damageHPMoveTime -= Time.deltaTime;
            if (damageHPMoveTime < 0)
            {
                DOTween.To(() => currentHP_Damage_Tween, (n) => currentHP_Damage_Tween = n, currentHP, damageDecreaseTime);
                damageHPMoveTime = 100000;
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
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            damageHPMoveTime = damageHPMoveTimeMAX;
            currentHP -= col.gameObject.GetComponent<Enemy>().damage;
            Destroy(col.gameObject);
        }
    }
}
