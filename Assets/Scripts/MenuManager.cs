using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;

    [SerializeField] Animator menuAnimator;
    [SerializeField] string playGameAnimationTrigger;
    [SerializeField] string returnMainMenuTrigger;
    [SerializeField] string retryGameTrigger;
    [Space]
    [SerializeField] Animator settingsAnimator;
    [SerializeField] string toogleSettingsBool;
    bool showSettings = false;
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
    [SerializeField] int[] colorShiftValues;
    [SerializeField] int defaultColorIndex;
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
        int gameColor = PlayerPrefs.GetInt(playerGameColorConfigkey, defaultColorIndex);
        PostProcesingManager.sharedInstance.ChangeHueShiftInProfile(colorShiftValues[gameColor]);
    }

    private void Start()
    {
        UpdateGameSettingsInfoDisplay();
    }

    public void PlayButtonPresed()
    {
        menuAnimator.SetTrigger(playGameAnimationTrigger);
    }

    public void StartGame()
    {
        GameManager.sharedInstance.StartNewMatch();
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void Return2MainMenu()
    {
        menuAnimator.SetTrigger(returnMainMenuTrigger);
    }

    public void RetryGame()
    {
        menuAnimator.SetTrigger(retryGameTrigger);
    }

    public void ToggleSettings()
    {
        showSettings = !showSettings;
        settingsAnimator.SetBool(toogleSettingsBool, showSettings);
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
    }
}
