using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnamyManagerStage_8 : MonoBehaviour
{
    public int enemyCount;
    public int enemyTypeChange;
    private float[] time;
    private float hp, maxHp;
    private float hpPercentage;
    public float icon_radius;
    private float y_max, y_min;
    private float x_max, x_min;
    private GameObject player_;
    private Player player;
    //public GameObject Enemy_Fall, Enemy_FixedSpeed, Enemy_CAR, Enemy_Homing, Enemy_Path, Enemy_ZigzagFall, Enemy_Random;
    public GameObject[] enemys = new GameObject[7];//enemyCount == enemys.Length
    public float[] timer = new float[7];//enemyCount == timer.Length
    private float[] timer_ = new float[7];//enemyCount == timer_.Length
    private int[] counter;
    public int[] counterMax = new int[7];//enemyCount == counterMax.Len
    public float[] levelup = new float[5];//levelup.Length == levelの数;
    private class RandomObjects{}
    private Rigidbody2D fall;
    private FixedSpeed fixedSpeed;
    private ChangingAirResistance car;
    private ZigzagFall zig;
    private Enemy_bn_Homing homing; 
    private Enemy_bn_path path;
    private Enemy_bn_random random;
    

    // Start is called before the first frame update
    void Start()
    {
        time = new float[enemyCount];
        counter = new int[enemyCount];
        y_max = 5f;
        y_min = -3.65f;
        x_max = 5f;
        x_min = -1.9f;
        player_ = GameObject.FindWithTag("Player");
        player = player_.GetComponent<Player>();
        maxHp = player.maxHP;
        fall = enemys[0].GetComponent<Rigidbody2D>();
        fixedSpeed = enemys[1].GetComponent<FixedSpeed>();
        car = enemys[2].GetComponent<ChangingAirResistance>();
        zig = enemys[3].GetComponent<ZigzagFall>();
        homing = enemys[4].GetComponent<Enemy_bn_Homing>();
        path = enemys[5].GetComponent<Enemy_bn_path>();
        random = enemys[6].GetComponent<Enemy_bn_random>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < time.Length; i++)
        {
            time[i] += Time.deltaTime;
        }
        hp = player.currentHP;
        hp = 2;
        maxHp = 3;
        hpPercentage = hp / maxHp * 100;
        switch (hpPercentage)
        {
            case float threshold_value when threshold_value > 90:
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] > timer[i]*levelup[0])
                    {
                        if (counter[i] < counterMax[i])
                        {
                            if (i < enemyTypeChange)
                            {
                                Pop(enemys[i], RandPos());
                                counter[i]++;
                            }
                            else
                            {
                                Pop(enemys[i], RandPosInField());
                                counter[i]++;
                            }
                        }
                    }
                    time[i] = 0;
                }
                break;
            case float threshold_value when threshold_value > 75:
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] > timer[i]*levelup[1])
                    {
                        if (counter[i] < counterMax[i])
                        {
                            if (i < enemyTypeChange)
                            {
                                Pop(enemys[i], RandPos());
                                counter[i]++;
                            }
                            else
                            {
                                Pop(enemys[i], RandPosInField());
                                counter[i]++;
                            }
                        }
                    }
                    time[i] = 0;
                }
                break;
            case float threshold_value when threshold_value > 50:
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i]*levelup[2]);
                    if (time[i] > timer_[i])
                    {
                        if (counter[i] < counterMax[i])
                        {
                            if (i < enemyTypeChange)
                            {
                                Pop(enemys[i], RandPos());
                                counter[i]++;
                            }
                            else
                            {
                                Pop(enemys[i], RandPosInField());
                                counter[i]++;
                            }
                        }
                    }
                    time[i] = 0;
                }
                break;
            case float threshold_value when threshold_value > 30:
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i]*levelup[3]);
                    RandomMaker();
                    if (time[i] > timer_[i])
                    {
                        if (counter[i] < counterMax[i])
                        {
                            if (i < enemyTypeChange)
                            {
                                Pop(enemys[i], RandPos());
                                counter[i]++;
                            }
                            else
                            {
                                Pop(enemys[i], RandPosInField());
                                counter[i]++;
                            }
                        }
                    }
                    time[i] = 0;
                }
                break;
            case float threshold_value when threshold_value > 10:
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i]*levelup[4]);
                    RandomMaker();
                    if (time[i] > timer_[i])
                    {
                        if (counter[i] < counterMax[i])
                        {
                            if (i < enemyTypeChange)
                            {
                                Pop(enemys[i], RandPos());
                                counter[i]++;
                            }
                            else
                            {
                                Pop(enemys[i], RandPosInField());
                                counter[i]++;
                            }
                        }
                    }
                    time[i] = 0;
                }
                break;
            default:
                break;
        }
    }
    GameObject Pop(GameObject obj, Vector2 pos)
    {
        return Instantiate(obj, pos, quaternion.identity);
    }
    Vector2 RandPos()
    {
        Vector2 spawnPoint = new(0, 0)
        {
            x = Random.Range(x_min + icon_radius, x_max - icon_radius),
            y = Random.Range(y_max + icon_radius, y_max + icon_radius * 2)
        };
        return spawnPoint;
    }
    Vector2 RandPosInField()
    {
        Vector2 spawnPoint = new(0, 0)
        {
            x = Random.Range(x_min + icon_radius, x_max - icon_radius),
            y = Random.Range(0f, 5f - icon_radius)
        };
        return spawnPoint;
    }
    void RandomMaker()
    {
        fall.gravityScale = Random.Range(1f,2f);
        fall.drag = Random.Range(0f,2f);
        fixedSpeed.targetVelocity = Random.Range(0.05f,0.2f);
        car.Appeartime = Random.Range(0.5f,1.5f);
        car.drag = Random.Range(6f,14f);
        zig.swingRange = Random.Range(3f,7f);
        zig.swingSpeed = Random.Range(5f,8f);
        zig.speed = Random.Range(-9f,-7f);
        homing.freezeTime = Random.Range(1f,4f);
        homing.enemySpeed = Random.Range(4f,7f);
        path.freezTime = Random.Range(0f,1.5f);
        path.actStartTime = path.freezTime + Random.Range(0f,1f);
        path.moveStartTime = path.actStartTime + Random.Range(2f,4f);
        path.enemySpeed = Random.Range(8f,15f);
        path.enemyAirResistance = Random.Range(0f,1.5f);
        random.freezeTime = Random.Range(0f,1.5f);
        random.maxLen = Random.Range(2f,5f);
    }
}
