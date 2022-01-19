using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Animator menuAnimator;
    [SerializeField] string playGameAnimationTrigger;
    [SerializeField] string returnMainMenuTrigger;
    [SerializeField] string retryGameTrigger;
    [Space]
    [SerializeField] Animator settingsAnimator;
    [SerializeField] string toogleSettingsBool;
    bool showSettings = false;

    
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
}
