using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Shot : MonoBehaviour
{
    public float damage;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 画面外に出たら削除する
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
