using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnamyManagerStage_7 : MonoBehaviour
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
                    time[i] = 0;
                }
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] > timer[i] * levelup[0])
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
                    time[i] = 0;
                }
                for (int i = 0; i < time.Length; i++)
                {
                    if (time[i] > timer[i] * levelup[1])
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
                    time[i] = 0;
                }
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i] * levelup[2]);
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
                    time[i] = 0;
                }
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i] * levelup[3]);
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
                    time[i] = 0;
                }
                for (int i = 0; i < time.Length; i++)
                {
                    timer_[i] = Random.Range(0f, timer[i] * levelup[4]);
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
            y = Random.Range(0f, 4.5f - icon_radius)
        };
        return spawnPoint;
    }
}
