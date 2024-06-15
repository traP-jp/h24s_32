using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 子供ショットがこれを中心にぐるぐる周りながら進む
public class Shot_Megahon : MonoBehaviour
{
    // oisu-1234が装填される予定
    public GameObject[] shotPrefabs;
    // 半径
    public float radius = 1.0f;
    private float speed;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        speed = GetComponent<Shot>().speed;
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = Vector2.up * speed;
    }
}
