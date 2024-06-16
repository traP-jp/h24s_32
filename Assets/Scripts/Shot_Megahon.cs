using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

// 子供ショットがこれを中心にぐるぐる周りながら進む
public class Shot_Megahon : MonoBehaviour
{
    // oisu-1234が装填される予定
    public GameObject[] shotPrefabs;
    // 半径
    public float radius = 1.0f;
    // 回転速度
    public float rotateSpeed = 10.0f;
    private float speed;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<Shot>().speed;
        // 子関係のshotオブジェクトをshotPrefabsに登録する
        for (int i = 0; i < transform.childCount; i++)
        {
            shotPrefabs = shotPrefabs.Append(transform.GetChild(i).gameObject).ToArray();
        }
        for (int i = 0; i < shotPrefabs.Length; i++)
        {
            // shotPrefabsを円形に配置する
            float angle = 360.0f / shotPrefabs.Length * i;
            shotPrefabs[i].transform.localPosition = new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
                0.0f
            );
            // shotPrefabsの向きを円形にする
            Vector2 direction = Vector2.Perpendicular(shotPrefabs[i].transform.localPosition).normalized;
            shotPrefabs[i].transform.up = direction;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 親でrigidbodyを使うと子がうまく動かない...
        transform.position += transform.up * speed * Time.deltaTime;
        // 子供オブジェクトを自分の周りを回転させる
        for (int i = 0; i < shotPrefabs.Length; i++)
        {
            GameObject shotPrefab = shotPrefabs[i];
            if (shotPrefab == null)
            {
                continue;
            }
            // 子と親からみた法線ベクトルを求める
            Vector2 direction = Vector2.Perpendicular(shotPrefab.transform.localPosition).normalized;
            shotPrefab.GetComponent<Rigidbody2D>().velocity = direction * rotateSpeed;
            // 向きを変える
            shotPrefab.transform.up = direction;
        }
        // 子供が全員消えたら親も消す
        if (shotPrefabs.All(shotPrefab => shotPrefab == null))
        {
            Destroy(gameObject);
        }
    }
}
