using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EnemyManagerStage_1 : MonoBehaviour
{
    private float time;
    public float spawn_cycle;
    public GameObject enemy_fall;
    private GameObject enemy_fall_;
    public GameObject enemy_fixedspeed;
    private GameObject enemy_fixedspeed_;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawn_cycle)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                Vector2 spawnPoint = new Vector2(0, 0);
                spawnPoint.x = Random.Range(-1.5f,4.6f);
                spawnPoint.y = Random.Range(-5f * 0.5f, 5f);//下限は？
                enemy_fall_ = Instantiate(enemy_fall, spawnPoint, quaternion.identity);
                time = 0;
            }
            else
            {
                Vector2 spawnPoint = new Vector2(0, 0);
                spawnPoint.x = Random.Range(-(5f - 0.5f), (5f - 0.5f));
                spawnPoint.y = Random.Range(-5f * 0.5f, 5f);//下限は？
                enemy_fixedspeed_ = Instantiate(enemy_fixedspeed, spawnPoint, quaternion.identity);
                time = 0;
            }
        }
    }
}
