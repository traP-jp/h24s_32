using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using DG.Tweening;
public class Enemy : MonoBehaviour
{
    public string IconName;
    GameObject oisuManager;
    StageController stageController;
    ScoreManager scoreManager;
    IconManager iconManager;
    Player player;
    public int damage = 1;
    public int Score = 100;
    bool isKilled = false;
    AudioSource SEController;
    [SerializeField] AudioClip KilledSE;
    [SerializeField] GameObject OisuEffect;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        oisuManager = GameObject.Find("OisuManager");
        stageController = GameObject.FindGameObjectWithTag("StageController").GetComponent<StageController>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = iconManager.Icons[Random.Range(0, iconManager.Icons.Length)];
        IconName = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
        transform.localScale = new Vector3(0, 0, 0);
        transform.DOScale(new Vector3(0.5f, 0.5f, 1), 0.4f);
        SEController = GameObject.FindGameObjectWithTag("SEController").GetComponent<AudioSource>();
    }
    void Update()
    {
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shot" && !isKilled)
        {
            if (!col.GetComponent<Shot>().isDied)
            {
                col.GetComponent<Shot>().isDied = true;
                GameObject go = Instantiate(OisuEffect);
                go.transform.position = transform.position;
                SEController.PlayOneShot(KilledSE);
                isKilled = true;
                player.OisuCharge();
                stageController.CountOisu();
                scoreManager.CallCombo(Score);
                if (oisuManager != null)
                {
                    oisuManager.GetComponent<OisuManager>().CallOisu(IconName);
                }
                if (col.GetComponent<Shot>().isPenetrate && col.GetComponent<Shot>().penetrateCount == 0)
                {
                    col.GetComponent<Shot>().penetrateCount++;
                }
                else
                {
                    Destroy(col.gameObject);
                }
                Destroy(gameObject);
            }
        }
    }
    void OnDestroy()
    {
        transform.DOKill();
    }
}
