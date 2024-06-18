using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using UnityEngine.SceneManagement;
public class StageController : MonoBehaviour
{
    public bool startGame = false;
    public int stageNumber = 1;
    public bool isPlayingStage = false;
    int currentOisuCount = 0;
    public int totalOisuCount = 0;
    public float limitTime = 30;
    [SerializeField] float leftTime;
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
    [SerializeField] RectTransform clearedWindow;
    [SerializeField] PowerUpManager powerUpManager;
    [SerializeField] Text ClearedText;
    [SerializeField] Image WhiteOut;
    [SerializeField] AudioSource BGMController;
    [SerializeField] AudioSource BGMController_Loop;
    [SerializeField] AudioClip BGM2;
    [SerializeField] AudioClip BGM_Failed;
    [SerializeField] GameOverController gameOverController;
    // Start is called before the first frame update
    void Start()
    {
        WhiteOut.DOFade(0, 0.5f).SetEase(Ease.Linear);
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
            if (leftTime < 0 && isPlayingStage)
            {
                isPlayingStage = false;
                if (currentOisuCount >= targetEnemies[stageNumber - 1] && !gameOverController.GameEnded)
                {
                    BGMController.DOFade(0, 1).SetEase(Ease.Linear);
                    DOVirtual.DelayedCall(1, () =>
                    {
                        BGMController.Stop();
                    });
                    BGMController_Loop.DOFade(1, 1).SetEase(Ease.Linear);
                    if (stageNumber > 8)
                    {
                        sceneCaller.UnloadStageScene(8);
                    }
                    else
                    {
                        sceneCaller.UnloadStageScene(stageNumber);
                    }
                    ClearedText.DOText("", 0).SetEase(Ease.Linear);
                    ClearedText.DOFade(0, 0);
                    clearedWindow.DOSizeDelta(new Vector2(1000, 150), moveTime * 2).SetEase(Ease.OutExpo);
                    ClearedText.DOText("Cleared!", moveTime * 2, true, ScrambleMode.All).SetEase(Ease.Linear);
                    ClearedText.DOFade(1, moveTime * 2);
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        clearedWindow.DOSizeDelta(new Vector2(1000, 0), moveTime * 2).SetEase(Ease.InExpo);
                    });
                    DOVirtual.DelayedCall(1.5f + moveTime * 2, () =>
                    {
                        powerUpManager.StartPowerUp();
                    });
                    DOVirtual.DelayedCall(2 + moveTime * 2, () =>
                    {
                        powerUpManager.isPowerUpActive = true;
                        powerUpManager.MovePowerUps(0);
                    });
                }
                else if (!gameOverController.GameEnded)
                {
                    BGMController.DOFade(0, 1).SetEase(Ease.Linear);
                    DOVirtual.DelayedCall(1, () =>
                    {
                        BGMController.Stop();
                    });
                    if (stageNumber > 8)
                    {
                        sceneCaller.UnloadStageScene(8);
                    }
                    else
                    {
                        sceneCaller.UnloadStageScene(stageNumber);
                    }
                    ClearedText.DOText("", 0).SetEase(Ease.Linear);
                    ClearedText.DOFade(0, 0);
                    clearedWindow.DOSizeDelta(new Vector2(1000, 150), moveTime * 2).SetEase(Ease.OutExpo);
                    ClearedText.DOText("Failed...", moveTime * 2, true, ScrambleMode.All).SetEase(Ease.Linear);
                    ClearedText.color = new Color(0.3f, 0.3f, 0.3f, 0);
                    ClearedText.DOFade(1, moveTime * 2);
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        clearedWindow.DOSizeDelta(new Vector2(1000, 0), moveTime * 2).SetEase(Ease.InExpo);
                    });
                    DOVirtual.DelayedCall(1.5f + moveTime * 2, () =>
                    {
                        gameOverController.GameEnd(false);
                    });
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
        if (!gameOverController.GameEnded)
        {
            currentOisuCount = 0;
            stageStartWindow.DOSizeDelta(new Vector2(1000, 500), 1f).SetEase(Ease.OutExpo);
            for (int i = 0; i < 4; i++)
            {
                stageStartWindow.transform.GetChild(i).GetComponent<Text>().DOFade(0, 0);
                int Delay = i;
                DOVirtual.DelayedCall(Delay * 0.2f, () =>
                {
                    stageStartWindow.transform.GetChild(Delay).GetComponent<Text>().DOFade(1, 0.5f);
                    if (stageNumber > 8)
                    {
                        switch (Delay)
                        {
                            case 0:
                                stageNumberText_start.DOText("Stage " + stageNumber.ToString("D"), 0.5f, true, ScrambleMode.All).SetEase(Ease.Linear);
                                break;
                            case 1:
                                channelNameText_start.DOText("#" + channelNames_Former[7] + channelNames_Latter[7], 1f, true, ScrambleMode.All).SetEase(Ease.Linear);
                                break;
                            case 2:
                                targetOisu_start.DOText("目標おいすー数", 0.5f, true, ScrambleMode.All).SetEase(Ease.Linear);
                                break;
                            case 3:
                                targetOisuText_start.DOText(targetEnemies[stageNumber - 1].ToString("D"), 0.5f, true, ScrambleMode.Numerals).SetEase(Ease.Linear);
                                break;
                        }
                    }
                    else
                    {
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
                    }
                });
            }
            DOVirtual.DelayedCall(2.5f, () =>
            {
                stageStartWindow.DOSizeDelta(new Vector2(1000, 0), 1f).SetEase(Ease.InExpo);
            });
            DOVirtual.DelayedCall(3.5f, () =>
            {
                if (stageNumber > 8)
                {
                    for (int i = 0; i < Mathf.Pow(2, stageNumber - 8); i++)
                    {


                        sceneCaller.LoadStageScene(8);
                    }

                    currentChannelNameText.DOText(channelNames_Former[7] + "<size=80><color=#49535B>" + channelNames_Latter[7] + "</color></size>", 1, true, ScrambleMode.All);
                }
                else
                {
                    sceneCaller.LoadStageScene(stageNumber);
                    currentChannelNameText.DOText(channelNames_Former[stageNumber - 1] + "<size=80><color=#49535B>" + channelNames_Latter[stageNumber - 1] + "</color></size>", 1, true, ScrambleMode.All);
                }
                isPlayingStage = true;
                leftTime = limitTime;

                if (stageNumber <= 7)
                {
                    nextChannelNameText.DOText(channelNames_Omited[stageNumber], 0.7f, true, ScrambleMode.All);
                }
                oisuTargetText.DOText("/" + targetEnemies[stageNumber - 1].ToString("D"), 0.5f, true, ScrambleMode.Numerals);
                stageText.DOText("ステージ" + stageNumber.ToString("D"), 0.5f, true, ScrambleMode.All);

                if (stageNumber > 4)
                {
                    BGMController.clip = BGM2;
                }
                BGMController.Play();
                BGMController_Loop.DOFade(0, 1).SetEase(Ease.Linear);
                BGMController.DOFade(1, 1).SetEase(Ease.Linear);
            });
        }
    }
}
