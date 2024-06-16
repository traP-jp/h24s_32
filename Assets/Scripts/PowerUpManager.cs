using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Burst.Intrinsics;
public class PowerUpManager : MonoBehaviour
{
    int chosenPowerUp = 1;
    bool isPowerUpActive = false;
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
        }
    }
    public void StartPowerUp()
    {
        isPowerUpActive = true;
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
}
