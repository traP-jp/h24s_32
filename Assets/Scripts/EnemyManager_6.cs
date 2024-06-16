
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyManager_6 : MonoBehaviour
{
    private float time;
    public float spawn_cycle;
    public GameObject enemy_fall;
    public GameObject enemy_fixedspeed;
    public GameObject enemy_ZigzagFall;
    public GameObject enemy_CAR;
    public GameObject enemy_Homing;
    public GameObject enemy_Path;
    public GameObject enemy_random;
    GameObject[] enemys;
    GameObject[] Enemys;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        
        enemys = new GameObject[] {enemy_fall,enemy_fixedspeed, enemy_ZigzagFall,enemy_CAR,enemy_Homing,enemy_Path,enemy_random }; 
        Enemys = new GameObject[7] ;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > spawn_cycle)
        {
            for(int i = 0; i < 2; i++)
            {   
                int s = Random.Range(0,6);
                Vector2 spawnPoint = new Vector2(0, 0);
                spawnPoint.x = Random.Range(-1.5f,4.6f);
                spawnPoint.y = Random.Range(-5f * 0.5f, 5f);//下限は？
                Debug.Log(s+" "+spawnPoint);
                Enemys[s] = Instantiate(enemys[s], spawnPoint, quaternion.identity);
            }
            time = 0;
        }
        
    }
}