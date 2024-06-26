using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Transactions;
public class Player : MonoBehaviour
{
    [SerializeField] AudioSource SEController;
    [SerializeField] GameObject kyamera;
    public int maxHP = 10;
    public int currentHP;
    public float megaPower_Max = 100;
    public float megaPower_Current = 80;
    public float megaPower_Get = 5;
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
    [SerializeField] GameObject megaPhoneShot;
    [SerializeField] GameObject homingShot;
    [SerializeField] GameObject threeWayShot;
    [SerializeField] Animator playerAnimator;
    Rigidbody2D rb;
    SpriteRenderer playerRenderer;
    bool RightPushed = false;
    bool LeftPushed = false;
    bool UpPushed = false;
    bool SpacePushed = false;
    bool EnterPushed = false;
    public float shotSpeedMultiply = 1;
    public bool isHomingActive = false;
    public float megaMultiply = 1;
    public float shotScaleMultiply = 1;
    public float coolTimeMultiply = 1;
    public float maxHPMultiply = 1;
    public bool isShotPenetrate = false;
    public float moveSpeedMultiply = 1;
    public bool is3wayActive = false;
    [SerializeField] StageController stageController;
    [SerializeField] AudioClip Damaged;
    [SerializeField] AudioClip Damaged2;
    [SerializeField] AudioClip Jump;
    [SerializeField] AudioClip Oisu;
    [SerializeField] AudioClip bigOisu;
    [SerializeField] GameOverController gameOverController;
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
                rb.position += new Vector2(-moveSpeed * Time.deltaTime * moveSpeedMultiply, 0);
                playerRenderer.flipX = false;
                if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    playerAnimator.SetTrigger("RunTrigger");
                }
            }
            else if (RightPushed && transform.position.x < rightPosLimit)
            {
                rb.position += new Vector2(moveSpeed * Time.deltaTime * moveSpeedMultiply, 0);
                playerRenderer.flipX = true;
                if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                {
                    playerAnimator.SetTrigger("RunTrigger");
                }
            }
        }
        else
        {
            moveFreezeTime -= Time.deltaTime;
        }
        if (coolTime < 0 && SpacePushed)
        {
            SEController.PlayOneShot(Oisu);
            if (isHomingActive)
            {
                playerAnimator.SetTrigger("OisuTrigger");
                GameObject Target = homingShot;
                if (is3wayActive)
                {
                    Target = threeWayShot;
                }
                Vector3 shotBasePos = transform.position;
                GameObject go = Instantiate(Target);
                if (is3wayActive)
                {
                    go.GetComponent<Shot_3Way>().shotPrefab = homingShot;

                }
                else
                {

                }
                go.transform.position = shotBasePos += ShotSpawnPos;
                moveFreezeTime = moveFreezeTimeLimit;
                coolTime = coolTimeMax * coolTimeMultiply;

            }
            else
            {
                playerAnimator.SetTrigger("OisuTrigger");
                GameObject Target = normalShot;
                if (is3wayActive)
                {
                    Target = threeWayShot;
                }
                Vector3 shotBasePos = transform.position;
                GameObject go = Instantiate(Target);
                go.transform.position = shotBasePos += ShotSpawnPos;
                moveFreezeTime = moveFreezeTimeLimit;
                coolTime = coolTimeMax * coolTimeMultiply;

            }
        }
        else if (coolTime > 0)
        {
            coolTime -= Time.deltaTime;
        }
        if (EnterPushed && megaPower_Current >= megaPower_Max)
        {
            SEController.PlayOneShot(bigOisu);
            kyamera.transform.DOComplete();
            kyamera.transform.DOShakePosition(0.5f, 0.4f, 20);
            megaPower_Current = 0;
            playerAnimator.SetTrigger("MegaPhoneTrigger");
            Vector3 shotBasePos = transform.position;
            GameObject go = Instantiate(megaPhoneShot);
            go.transform.position = shotBasePos += ShotSpawnPos;
            moveFreezeTime = moveFreezeTimeLimit;
            coolTime = coolTimeMax * coolTimeMultiply;
        }
        if (UpPushed && transform.position.y < groundPos + 0.1f)
        {
            //SEController.PlayOneShot(Jump);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacePushed = true;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            EnterPushed = true;
        }
    }
    void PushReset()
    {
        RightPushed = false;
        LeftPushed = false;
        UpPushed = false;
        SpacePushed = false;
        EnterPushed = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && stageController.isPlayingStage)
        {
            SEController.PlayOneShot(Damaged);
            SEController.PlayOneShot(Damaged2);
            kyamera.transform.DOShakePosition(0.5f, 0.4f, 20);
            damageHPMoveTime = damageHPMoveTimeMAX;
            currentHP -= col.gameObject.GetComponent<Enemy>().damage;
            Destroy(col.gameObject);
            if (currentHP <= 0)
            {
                gameOverController.GameEnd(false);
            }
        }
    }
    public void OisuCharge()
    {
        megaPower_Current += megaPower_Get * megaMultiply;
    }
}
