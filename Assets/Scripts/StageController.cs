using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
public class StageController : MonoBehaviour
{
    public bool startGame = false;
    int stageNumber = 1;
    public bool isPlayingStage = false;
    int currentOisuCount = 0;
    int totalOisuCount = 0;
    public float limitTime = 30;
    float leftTime;
    public int[] targetEnemies = new int[8];
    public string[] channelNames_Omited = new string[8]; //�ȗ������`�����l����(��Fg/t/karo)
    public string[] channelNames_Former = new string[8]; //�Ō�ȊO�̃`�����l����(��Fgps/times/)
    public string[] channelNames_Latter = new string[8];//�Ō�̃`�����l����(karo)
    [SerializeField] Text stageText;
    [SerializeField] Text oisuCountText;
    [SerializeField] Text oisuTargetText;
    [SerializeField] Text oisuTotalText;
    [SerializeField] Text leftTimeText;
    [SerializeField] Text nextChannelNameText;
    [SerializeField] Text currentChannelNameText;
    [SerializeField] RectTransform stageStartWindow;
    [SerializeField] Text stageNumberText_start;
    [SerializeField] Text channelNameText_start;
    [SerializeField] Text targetOisu_start;
    [SerializeField] Text targetOisuText_start;
    [SerializeField] SceneCaller sceneCaller;
    [SerializeField] float oisuCountTextPos_x_Before;
    [SerializeField] float oisuCountTextPos_x_After;
    [SerializeField] float moveTime = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        if (startGame)
        {
            StartStage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayingStage)
        {
            leftTime -= Time.deltaTime;
            leftTimeText.text = leftTime.ToString("F2");
            if (leftTime < 0)
            {
                isPlayingStage = false;
                if (currentOisuCount >= targetEnemies[stageNumber - 1])
                {

                }
            }
        }
    }
    public void CountOisu()
    {
        currentOisuCount++;
        totalOisuCount++;
        oisuCountText.DOFade(0, 0);
        oisuCountText.transform.DOKill();
        oisuCountText.transform.localPosition = new Vector3(oisuCountTextPos_x_Before, oisuCountText.transform.localPosition.y, 0);
        oisuCountText.transform.DOLocalMoveX(oisuCountTextPos_x_After, moveTime).SetEase(Ease.OutExpo);
        oisuCountText.DOFade(1, moveTime);
        oisuTotalText.text = "TOTAL " + totalOisuCount.ToString("D");
        oisuCountText.text = currentOisuCount.ToString("D");
    }
    public void StartStage()
    {



        stageStartWindow.DOSizeDelta(new Vector2(1000, 500), 1f).SetEase(Ease.OutExpo);
        for (int i = 0; i < 4; i++)
        {
            stageStartWindow.transform.GetChild(i).GetComponent<Text>().DOFade(0, 0);
            int Delay = i;
            DOVirtual.DelayedCall(Delay * 0.2f, () =>
            {
                stageStartWindow.transform.GetChild(Delay).GetComponent<Text>().DOFade(1, 0.5f);
                switch (Delay)
                {
                    case 0:
                        stageNumberText_start.DOText("Stage " + stageNumber.ToString("D"), 0.5f, true, ScrambleMode.All).SetEase(Ease.Linear);
                        break;
                    case 1:
                        channelNameText_start.DOText("#" + channelNames_Former[stageNumber - 1] + channelNames_Latter[stageNumber - 1], 1f, true, ScrambleMode.All).SetEase(Ease.Linear);
                        break;
                    case 2:
                        targetOisu_start.DOText("目標おいすー数", 0.5f, true, ScrambleMode.All).SetEase(Ease.Linear);
                        break;
                    case 3:
                        targetOisuText_start.DOText(targetEnemies[stageNumber - 1].ToString("D"), 0.5f, true, ScrambleMode.Numerals).SetEase(Ease.Linear);
                        break;
                }
            });
        }
        DOVirtual.DelayedCall(2.5f, () =>
        {
            stageStartWindow.DOSizeDelta(new Vector2(1000, 0), 1f).SetEase(Ease.InExpo);
        });
        DOVirtual.DelayedCall(3.5f, () =>
        {
            sceneCaller.LoadStageScene(stageNumber);
            isPlayingStage = true;
            leftTime = limitTime;
            currentChannelNameText.DOText(channelNames_Former[stageNumber - 1] + "<size=80><color=#49535B>" + channelNames_Latter[stageNumber - 1] + "</color></size>", 1, true, ScrambleMode.All);
            if (stageNumber <= 7)
            {
                nextChannelNameText.DOText(channelNames_Omited[stageNumber], 0.7f, true, ScrambleMode.All);
            }
            oisuTargetText.DOText("/" + targetEnemies[stageNumber - 1].ToString("D"), 0.5f, true, ScrambleMode.Numerals);
            stageText.DOText("ステージ" + stageNumber.ToString("D"), 0.5f, true, ScrambleMode.All);
        });
    }
}