using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_3Way : MonoBehaviour
{
    // 弾のプレハブ
    public GameObject shotPrefab;
    // 弾の発射角度
    public float shotAngle = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(
                shotPrefab,
                transform.position,
                Quaternion.Euler(0.0f, 0.0f, shotAngle * (i - 1))
            );
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 3弾発射した後は削除する
        Destroy(gameObject);
    }
}
