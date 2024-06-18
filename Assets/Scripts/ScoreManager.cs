using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int totalScore = 0;
    int totalScore_Tween = 0;
    int comboCount = 0;
    [SerializeField] StageController stageController;
    [SerializeField] Text totalScoreText;
    [SerializeField] Text addedScoreText;
    [SerializeField] Text comboText;
    [SerializeField] Text comboMultiplyText;
    [SerializeField] Image comboGauge;
    public float comboTimeMax = 10;
    float comboTime = 0.001f;
    float moveTime = 0.3f;
    public float scoreMultiply = 1;
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
        totalScore += (int)(scoreMultiply * value * (10 + (comboCount / 10)) / 10);
        comboMultiplyText.text = "×" + ((float)(10 + (comboCount / 10)) / 10).ToString("F1");
        comboText.text = comboCount.ToString("D");
        DOTween.To(() => totalScore_Tween, (n) => totalScore_Tween = n, totalScore, 0.5f);
        addedScoreText.text = "+" + ((int)(scoreMultiply * value * (10 + (comboCount / 10)) / 10)).ToString("D");
        comboTime = comboTimeMax;
        addedScoreText.DOKill();
        addedScoreText.DOFade(0, 0);
        addedScoreText.DOFade(1, moveTime);
        addedScoreText.transform.DOKill();
        addedScoreText.transform.localPosition = new Vector3(35, addedScoreText.transform.localPosition.y, 0);
        addedScoreText.transform.DOLocalMoveX(49.33f, moveTime * 2).SetEase(Ease.OutExpo);
        comboText.DOKill();
        comboText.DOFade(0, 0);
        comboText.DOFade(1, moveTime);
        comboText.transform.DOKill();
        comboText.transform.localPosition = new Vector3(100, comboText.transform.localPosition.y, 0);
        comboText.transform.DOLocalMoveX(116.677f, moveTime * 2).SetEase(Ease.OutExpo);
        if (comboCount % 10 == 0)
        {
            comboMultiplyText.DOKill();
            comboMultiplyText.DOFade(0, 0);
            comboMultiplyText.DOFade(1, moveTime);
            comboMultiplyText.transform.DOKill();
            comboMultiplyText.transform.localPosition = new Vector3(50, comboMultiplyText.transform.localPosition.y, 0);
            comboMultiplyText.transform.DOLocalMoveX(63.97696f, moveTime * 2).SetEase(Ease.OutExpo);
        }
    }
}
