using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Shot : MonoBehaviour
{
    public float damage;
    public float speed = 5.0f;

    Player player;

    // 貫通するかどうか
    public bool isPenetrate = false;
    // 貫通した回数
    public int penetrateCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        float changedScale = transform.localScale.x * player.shotScaleMultiply;
        transform.localScale = new Vector3(changedScale, changedScale, 1);
        speed *= player.shotSpeedMultiply;
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
