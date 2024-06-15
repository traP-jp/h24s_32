using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatusView : MonoBehaviour
{
    public Player player;
    [SerializeField] Image HPBar;
    [SerializeField] Image HPBar_damage;
    [SerializeField] Image megaGauge;
    [SerializeField] Image coolTimeGauge;
    [SerializeField] Text coolTimeText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPBar.fillAmount = (float)player.currentHP / player.maxHP;
        HPBar_damage.fillAmount = player.currentHP_Damage_Tween / player.maxHP;
        coolTimeGauge.fillAmount = (player.coolTimeMax - player.coolTime) / player.coolTimeMax;
        if (player.coolTime > 0)
        {
            coolTimeText.text = player.coolTime.ToString("F2");
        }
        else
        {
            coolTimeText.text = "READY";
        }
    }
}
