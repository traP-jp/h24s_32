using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class EnemyManagerStage_1 : MonoBehaviour
{
    private float time;
    public float spawn_cycle;
    public float icon_radius;
    private float y_max, y_min;
    private float x_max, x_min;
    public GameObject enemy_fall;
    private GameObject enemy_fall_;
    public GameObject enemy_fixedspeed;
    private GameObject enemy_fixedspeed_;

    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        y_max = 5f;
        y_min = -3.65f;
        x_max = 5f;
        x_min = -1.9f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawn_cycle)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                Vector2 spawnPoint = new(0, 0)
                {
                    x = Random.Range(x_min + icon_radius, x_max - icon_radius),
                    y = Random.Range(y_max + icon_radius, y_max + icon_radius * 2)
                };
                enemy_fall_ = Instantiate(enemy_fall, spawnPoint, quaternion.identity);
                time = 0;
            }
            else
            {
                Vector2 spawnPoint = new(0, 0)
                {
                    x = Random.Range(x_min + icon_radius, x_max - icon_radius),
                    y = Random.Range(y_max + icon_radius, y_max + icon_radius * 2)
                };
                enemy_fixedspeed_ = Instantiate(enemy_fixedspeed, spawnPoint, quaternion.identity);
                time = 0;
            }
        }
    }
}
