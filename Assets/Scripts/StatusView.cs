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
    [SerializeField] Text MegaText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HPBar.fillAmount = (float)player.currentHP / (player.maxHP * player.maxHPMultiply);
        HPBar_damage.fillAmount = player.currentHP_Damage_Tween / (player.maxHP * player.maxHPMultiply);
        coolTimeGauge.fillAmount = ((player.coolTimeMax * player.coolTimeMultiply) - player.coolTime) / (player.coolTimeMax * player.coolTimeMultiply);
        megaGauge.fillAmount = player.megaPower_Current / player.megaPower_Max;
        if (player.megaPower_Current >= player.megaPower_Max)
        {
            MegaText.text = "READY!!";
        }
        else
        {
            MegaText.text = "";
        }
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
