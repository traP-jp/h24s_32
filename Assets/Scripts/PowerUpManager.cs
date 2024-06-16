using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting.Antlr3.Runtime;
using System.Runtime.InteropServices;
public class PowerUpManager : MonoBehaviour
{
    int chosenPowerUp = 1;
    public bool isPowerUpActive = false;
    public int[] PowerUps = new int[3];
    public Text[] PowerUpNameTexts = new Text[3];
    public Text[] PowerUPContentTexts = new Text[3];
    public Image[] PowerUpImages = new Image[3];
    public GameObject[] PowerUpWindows = new GameObject[3];
    public Sprite[] powerUpIcons = new Sprite[11];
    public string[] powerUpNames = new string[11];
    public string[] powerUpContents = new string[11];
    public bool[] powerUpNotSecondChoice = new bool[11]
;
    bool[] PowerUps_Chosen = new bool[11];
    public int[] WeightsOfPowerUp = new int[11];
    [SerializeField] Player player;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] GameObject Kinano;
    [SerializeField] StageController stageController;
    [SerializeField] Text PowerUpChooseText;
    [SerializeField] Text PowerUpGuidanceText;
    [SerializeField] GameOverController gameOverController;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isPowerUpActive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && chosenPowerUp <= 1)
            {
                MovePowerUps(1);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && chosenPowerUp >= 1)
            {
                MovePowerUps(-1);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DecidePowerUp();
                isPowerUpActive = false;
            }
        }
    }
    public void StartPowerUp()
    {
        PowerUpGuidanceText.DOFade(0, 0);
        PowerUpGuidanceText.DOFade(1, 0.5f);
        PowerUpChooseText.DOFade(0, 0);
        PowerUpChooseText.DOFade(1, 0.5f);
        for (int i = 0; i < 3; i++)
        {
            PowerUpWindows[i].transform.localPosition = new Vector3(PowerUpWindows[i].transform.localPosition.x, -500, 0);
            PowerUpWindows[i].transform.DOLocalMoveY(-70, 0.5f);
            PowerUps[i] = ChoicePowerUp();
            PowerUpNameTexts[i].text = powerUpNames[PowerUps[i]];
            PowerUpImages[i].sprite = powerUpIcons[PowerUps[i]];
            PowerUPContentTexts[i].text = powerUpContents[PowerUps[i]];
            PowerUpImages[i].SetNativeSize();
        }
    }
    public int ChoicePowerUp()
    {
        int totalweight = 0;
        int chosen = 0;
        for (int i = 0; i < powerUpIcons.Length; i++)
        {
            totalweight += WeightsOfPowerUp[i];
        }
        int ChosenNumber = Random.Range(0, totalweight);
        Debug.Log(ChosenNumber);
        int currentTotalWeight = 0;
        for (int j = 0; j < powerUpIcons.Length; j++)
        {
            currentTotalWeight += WeightsOfPowerUp[j];
            if (ChosenNumber < currentTotalWeight && (!powerUpNotSecondChoice[j] || !PowerUps_Chosen[j]))
            {
                chosen = j;
                PowerUps_Chosen[j] = true;
                j = 100000;
            }
        }
        return chosen;
    }
    public void MovePowerUps(int moveValue)
    {
        PowerUpWindows[chosenPowerUp].transform.DOLocalMoveY(-70, 0.3f);
        PowerUpImages[chosenPowerUp].DOColor(new Color(0, 0.35f, 0.67f, 1), 0.3f);
        chosenPowerUp += moveValue;
        PowerUpWindows[chosenPowerUp].transform.DOLocalMoveY(-20, 0.3f);
        PowerUpImages[chosenPowerUp].DOColor(new Color(0.95f, 0.6f, 0.3f, 1), 0.3f);
    }
    public void DecidePowerUp()
    {
        PowerUpGuidanceText.DOFade(0, 0.5f);
        PowerUpChooseText.DOFade(0, 0.5f);
        switch (PowerUps[chosenPowerUp])
        {
            case 0:
                player.coolTimeMultiply *= 0.5f;
                break;
            case 1:
                player.shotSpeedMultiply += 1;
                break;
            case 2:
                player.currentHP = (int)(player.maxHP * player.maxHPMultiply);
                player.currentHP_Damage_Tween = (int)(player.maxHP * player.maxHPMultiply);
                break;
            case 3:
                scoreManager.scoreMultiply += 0.5f;
                break;
            case 4:
                player.shotScaleMultiply += 0.5f;
                break;
            case 5:
                player.isHomingActive = true;
                break;
            case 6:
                player.maxHPMultiply += 0.5f;
                player.currentHP += player.maxHP / 2;
                player.currentHP_Damage_Tween += player.maxHP / 2;
                break;
            case 7:
                GameObject go = Instantiate(Kinano);
                go.transform.position = new Vector3(-1, 0, 0);
                break;
            case 8:
                player.isShotPenetrate = true;
                break;
            case 9:
                player.moveSpeedMultiply += 0.5f;
                break;
            case 10:
                player.is3wayActive = true;
                break;
        }
        for (int i = 0; i < 3; i++)
        {
            if (i != chosenPowerUp)
            {
                PowerUpWindows[i].transform.DOLocalMoveY(-500, 0.5f);
            }
        }
        DOVirtual.DelayedCall(1, () =>
        {
            PowerUpWindows[chosenPowerUp].transform.DOLocalMoveY(500, 0.5f).SetEase(Ease.InSine);
        });
        DOVirtual.DelayedCall(1.5f, () =>
        {
            stageController.stageNumber++;
            if (stageController.stageNumber != 8)
            {
                stageController.StartStage();
            }
            else
            {
                gameOverController.GameEnd(true);
            }
        });
    }
}
