using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string IconName;
    GameObject oisuManager;
    StageController stageController;
    ScoreManager scoreManager;
    public int damage = 1;
    public int Score = 100;
    void Start()
    {
        oisuManager = GameObject.Find("OisuManager");
        stageController = GameObject.FindGameObjectWithTag("StageController").GetComponent<StageController>();
        scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shot")
        {
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
