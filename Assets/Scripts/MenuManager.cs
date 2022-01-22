using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    [Header("UI Components")]
    [SerializeField] string scorePrefix;
    [SerializeField] TextMeshProUGUI headerScoreTextComponent;
    [SerializeField] TextMeshProUGUI endGameScoreTextComponent;
    [SerializeField] string highScorePrefix;
    [SerializeField] TextMeshProUGUI headerHighScoreTextComponent;
    [SerializeField] TextMeshProUGUI endameHighScoreTextComponent;
    [SerializeField] TextMeshProUGUI[] AllGameWindowsHeaders;
    [SerializeField] Color DarkFontColor;
    [SerializeField] Color LightFontColor;

    [Header("PlayerSettings")]
    public string playerAppleAmountConfigkey;
    [SerializeField] int minAppleAmount, maxAppleAmount;
    [SerializeField] TextMeshProUGUI AppleAmountDisplayText;
    [Space]
    public string playerGameSpeedConfigkey;
    public int minGameSpeed, maxGameSpeed;
    public int defaultGameSpeed;
    public float maxFrequency;
    [SerializeField] TextMeshProUGUI gameSpeedDisplayText;
    [Space]
    public string playerGameColorConfigkey;
    public int[] colorShiftValues;
    public int defaultColorIndex;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        UpdateGameSettingsInfoDisplay();
    }

    public void PlayButtonPresed()
    {
        WindowsManager.sharedInstance.HideAllWindows();
        WindowsManager.sharedInstance.HidePlayButton();
        StartGame();
    }

    public void StartGame()
    {
        GameManager.sharedInstance.StartNewMatch();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void IncreaseAppleAmount()
    {
        int AppleAmount = PlayerPrefs.GetInt(playerAppleAmountConfigkey, minAppleAmount);
        AppleAmount++;
        if (AppleAmount > maxAppleAmount)
        {
            AppleAmount = minAppleAmount;
        }
        PlayerPrefs.SetInt(playerAppleAmountConfigkey, AppleAmount);

        //Update UI
        AppleAmountDisplayText.text = AppleAmount.ToString();
    }
    public void DecreaseAppleAmount()
    {
        int AppleAmount = PlayerPrefs.GetInt(playerAppleAmountConfigkey, minAppleAmount);
        AppleAmount--;
        if (AppleAmount < minAppleAmount)
        {
            AppleAmount = maxAppleAmount;
        }
        PlayerPrefs.SetInt(playerAppleAmountConfigkey, AppleAmount);

        //Update UI
        AppleAmountDisplayText.text = AppleAmount.ToString();
    }

    public void IncreaseGameSpeed()
    {
        int gameSpeed = PlayerPrefs.GetInt(playerGameSpeedConfigkey, defaultGameSpeed);
        gameSpeed++;
        if (gameSpeed > maxGameSpeed)
        {
            gameSpeed = minGameSpeed;
        }
        PlayerPrefs.SetInt(playerGameSpeedConfigkey, gameSpeed);

        //Update UI
        gameSpeedDisplayText.text = gameSpeed.ToString();
    }
    public void DecreaseGameSpeed()
    {
        int gameSpeed = PlayerPrefs.GetInt(playerGameSpeedConfigkey, defaultGameSpeed);
        gameSpeed--;
        if (gameSpeed < minGameSpeed)
        {
            gameSpeed = maxGameSpeed;
        }
        PlayerPrefs.SetInt(playerGameSpeedConfigkey, gameSpeed);

        //Update UI
        gameSpeedDisplayText.text = gameSpeed.ToString();
    }

    public void IncreaseGameColor()
    {
        int gameColor = PlayerPrefs.GetInt(playerGameColorConfigkey, defaultColorIndex);
        gameColor++;
        if (gameColor > colorShiftValues.Length - 1)
        {
            gameColor = 0;
        }

        PlayerPrefs.SetInt(playerGameColorConfigkey,gameColor);

        //Update view
        PostProcesingManager.sharedInstance.ChangeHueShiftInProfile(colorShiftValues[gameColor]);
    }
    public void DecreaseGameColor()
    {
        int gameColor = PlayerPrefs.GetInt(playerGameColorConfigkey, defaultColorIndex);
        gameColor--;
        if (gameColor < 0)
        {
            gameColor = colorShiftValues.Length - 1;
        }

        PlayerPrefs.SetInt(playerGameColorConfigkey, gameColor);

        //Update view
        PostProcesingManager.sharedInstance.ChangeHueShiftInProfile(colorShiftValues[gameColor]);
    }

    private void UpdateGameSettingsInfoDisplay()
    {
        int AppleAmount = PlayerPrefs.GetInt(playerAppleAmountConfigkey, minAppleAmount);
        AppleAmountDisplayText.text = AppleAmount.ToString();

        int gameSpeed = PlayerPrefs.GetInt(playerGameSpeedConfigkey, defaultGameSpeed);
        gameSpeedDisplayText.text = gameSpeed.ToString();

        int gameColor = PlayerPrefs.GetInt(playerGameColorConfigkey, defaultColorIndex);
        PostProcesingManager.sharedInstance.ChangeHueShiftInProfile(colorShiftValues[gameColor]);
    }

    public void UpdateScoreDisplay()
    {
        int score = GameManager.sharedInstance.score;
        string highScoreKey = GameManager.sharedInstance.playerHighScoreKey;
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        //Ingame UI
        headerScoreTextComponent.text = $"{scorePrefix} {score}";
        headerHighScoreTextComponent.text = $"{highScorePrefix} {PlayerPrefs.GetInt(highScoreKey, 0)}";

        //GameOver UI delete
        endGameScoreTextComponent.text = $"{scorePrefix} {score}";
        endameHighScoreTextComponent.text = $"{highScorePrefix} {highScore}";
    }

    public void ChangeHeadersFont2Dark()
    {
        foreach (var header in AllGameWindowsHeaders)
        {
            header.color = DarkFontColor;
        }
    }

    public void ChangeHeadersFont2Light()
    {
        foreach (var header in AllGameWindowsHeaders)
        {
            header.color = LightFontColor;
            if(header.gameObject.name.Equals("PlayButtonText (TMP)"))
            {
                header.color = Color.red;
            }
        }
    }
}
