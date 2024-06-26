using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Bomb : MonoBehaviour
{
    // 爆発までの予兆スピード
    public float preExplosionSpeed = 1.0f;
    // 爆発までの予兆時間
    public float preExplosionTime = 1.0f;
    // 爆発までの段階
    public int preExplosionStep = 5;
    // 現在の段階
    public int currentStep = 0;
    // 爆発までの時間
    public float explosionTime = 3.0f;
    public ParticleSystem explosionEffect;
    private float speed;
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private AudioSource _audio;
    [SerializeField]
    private AudioClip _bombSE;
    private float time = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        speed = GetComponent<Shot>().speed;
        _rb = GetComponent<Rigidbody2D>();       
        _collider = GetComponent<Collider2D>();
        _audio = GameObject.FindGameObjectWithTag("SEController").GetComponent<AudioSource>();
        _audio.PlayOneShot(_bombSE);
        GetComponent<Shot>().isPenetrate = true;
    }

    // Update is called once per frame
    void Update()
    {
        // 段階までの時間までに応じて段階を上げる
        float deltaExplostionTime = explosionTime / preExplosionStep;
        if (time > deltaExplostionTime * (currentStep + 1))
        {
            currentStep++;
        }

        // 爆弾のように徐々に大きくなる
        float scaleChange = Mathf.PingPong(Time.time * preExplosionSpeed, preExplosionTime) + currentStep;
        transform.localScale = new Vector3(scaleChange, scaleChange, 1.0f);
        // あたり判定のサイズも変更する
        _collider.transform.localScale = new Vector3(scaleChange, scaleChange, 1.0f);

        // 直進する
        _rb.velocity = transform.up * speed;

        // 爆発する
        if (currentStep >= preExplosionStep)
        {
            // 爆発エフェクトをInstantiateする
            var effect = Instantiate(explosionEffect);
            effect.transform.position = transform.position;
            effect.Play();
            Destroy(gameObject);
        }
        time += Time.deltaTime;
    }
}
