using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string IconName;
    GameObject oisuManager;
    public int damage = 1;
    void Start()
    {
        oisuManager = GameObject.Find("OisuManager");
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shot")
        {
            if (oisuManager != null)
            {
                oisuManager.GetComponent<OisuManager>().CallOisu(IconName);
            }
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
