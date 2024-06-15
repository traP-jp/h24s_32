using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int totalScore = 0;
    int totalScore_Tween = 0;
    int comboCount = 0;
    [SerializeField] StageController stageController;
    [SerializeField] Text totalScoreText;
    [SerializeField] Text addedScoreText;
    [SerializeField] Text comboText;
    [SerializeField] Text comboMultiplyText;
    [SerializeField] Image comboGauge;
    float comboTimeMax = 10;
    float comboTime = 0.001f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (comboTime > 0 && stageController.isPlayingStage)
        {
            comboTime -= Time.deltaTime;
            comboGauge.fillAmount = comboTime / comboTimeMax;
            if (comboTime <= 0)
            {
                comboCount = 0;
                comboText.text = comboCount.ToString("D");
                comboMultiplyText.text = "×" + ((float)(10 + (comboCount / 10)) / 10).ToString("F1");
            }
        }
        totalScoreText.text = totalScore_Tween.ToString("D8");
    }
    public void CallCombo(int value)
    {
        comboCount++;
        totalScore += value * (10 + (comboCount / 10)) / 10;
        comboMultiplyText.text = "×" + ((float)(10 + (comboCount / 10)) / 10).ToString("F1");
        comboText.text = comboCount.ToString("D");
        DOTween.To(() => totalScore_Tween, (n) => totalScore_Tween = n, totalScore, 0.5f);
        addedScoreText.text = "+" + (value * (10 + (comboCount / 10)) / 10).ToString("D");
        comboTime = comboTimeMax;
    }
}
