using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyManager_4 : MonoBehaviour
{
    private float time;
    public float spawn_cycle;
    public GameObject enemy_fall;
    private GameObject _enemy_fall;
    public GameObject enemy_fixedspeed;
    private GameObject _enemy_fixedspeed;
    public GameObject enemy_Path;
    private GameObject _enemy_Path;

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
            if (Random.Range(0f, 1f) < 1/3f)
            {
                Vector2 spawnPoint = new Vector2(0, 0);
                spawnPoint.x = Random.Range(-1.9f + 0.5f,5f-0.5f);
                spawnPoint.y = Random.Range(5f + 0.5f, 5f + 1f);//下限は？
                _enemy_fall = Instantiate(enemy_fall, spawnPoint, quaternion.identity);
                time = 0;
            }
            else if(Random.Range(0f, 1f) < 1/3f)
            {
                Vector2 spawnPoint = new Vector2(0, 0);
               spawnPoint.x = Random.Range(-1.9f + 0.5f,5f-0.5f);
                spawnPoint.y = Random.Range(5f + 0.5f, 5f + 1f);//下限は？
                _enemy_fixedspeed = Instantiate(enemy_fixedspeed, spawnPoint, quaternion.identity);
                time = 0;
            }else 
            {
                Vector2 spawnPoint = new Vector2(0, 0);
                spawnPoint.x = Random.Range(-1.9f + 0.5f,5f-0.5f);
                spawnPoint.y = Random.Range(0f, 4.5f - 0.5f);//下限は？
                _enemy_Path = Instantiate(enemy_Path, spawnPoint, quaternion.identity);
                time = 0;
            }
        }
    }
}