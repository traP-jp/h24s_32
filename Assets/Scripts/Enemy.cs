using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        oisuManager = GameObject.Find("OisuManager");
        stageController = GameObject.FindGameObjectWithTag("StageController").GetComponent<StageController>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        iconManager = GameObject.FindGameObjectWithTag("IconManager").GetComponent<IconManager>();
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = iconManager.Icons[Random.Range(0, iconManager.Icons.Length)];
        IconName = transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shot")
        {
            player.OisuCharge();
            stageController.CountOisu();
            scoreManager.CallCombo(Score);
            if (oisuManager != null)
            {
                oisuManager.GetComponent<OisuManager>().CallOisu(IconName);
            }
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
