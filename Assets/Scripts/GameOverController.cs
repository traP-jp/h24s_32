using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using unityroom.Api;
public class GameOverController : MonoBehaviour
{
    public bool GameEnded = false;
    bool Pushed = false;
    bool isEndless = false;
    [SerializeField] StageController stageController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] Image WhiteOut_gameEnd;
    [SerializeField] Image ResultDisplay;
    [SerializeField] Text OisuCountText;
    [SerializeField] Text ScoreText;
    [SerializeField] Text KilledChannelText;
    [SerializeField] Text KilledChannelNameText;
    [SerializeField] Sprite Result_GameOver;
    [SerializeField] Sprite Result_Cleared;
    [SerializeField] Image WhiteOut;
    [SerializeField] Text SpaceToRetryText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Pushed && GameEnded && Input.GetKeyDown(KeyCode.Space))
        {
            if (isEndless)
            {
                GameEnded = false;
                WhiteOut_gameEnd.DOFade(0, 1);
                ScoreText.text = "スコア  " + scoreManager.totalScore.ToString("D8");
                OisuCountText.text = stageController.totalOisuCount.ToString("D");
                ResultDisplay.transform.DOLocalMoveY(800, 0.5f);
                stageController.stageNumber++;
                stageController.StartStage();
            }
            else
            {
                WhiteOut.DOFade(1, 0.5f);
                Pushed = true;
                DOVirtual.DelayedCall(0.5f, () =>
                {
                    SceneManager.LoadScene("GameScene");
                });
            }
        }
    }
    public void GameEnd(bool Cleared)
    {
        GameEnded = true;
        WhiteOut_gameEnd.DOFade(0.2f, 1);
        if (Cleared)
        {
            KilledChannelNameText.DOFade(0, 0);
            KilledChannelText.DOFade(0, 0);
            ResultDisplay.sprite = Result_Cleared;
            SpaceToRetryText.text = "スペースでエンドレス";
            isEndless = true;
        }
        else
        {
            isEndless = false;
            SpaceToRetryText.text = "スペースでリトライ";
            ResultDisplay.sprite = Result_GameOver;
        }
        UnityroomApiClient.Instance.SendScore(1, scoreManager.totalScore, ScoreboardWriteMode.HighScoreDesc);
        ScoreText.text = "スコア  " + scoreManager.totalScore.ToString("D8");
        OisuCountText.text = stageController.totalOisuCount.ToString("D");
        if (stageController.stageNumber > 8)
        {
            KilledChannelNameText.text = "#general　ステージ" + stageController.stageNumber.ToString("D");
        }
        else
        {
            KilledChannelNameText.text = "#" + stageController.channelNames_Former[stageController.stageNumber - 1] + stageController.channelNames_Latter[stageController.stageNumber - 1] + "　ステージ" + stageController.stageNumber.ToString("D");
        }
        ResultDisplay.transform.DOLocalMoveY(0, 0.5f);
    }
}
