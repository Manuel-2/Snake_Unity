using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    public static WindowsManager sharedInstance;

    [SerializeField] GameObject Settings;
    bool settingsWindowIsActive;
    [SerializeField] GameObject Creddits;
    bool credditsWindowIsActive;
    [SerializeField] GameObject Controls;
    bool controlsWindowIsActive;

    [SerializeField] GameObject GameOverWindow;
    [SerializeField] GameObject playButton;

    private void Awake()
    {
        if(sharedInstance== null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }


        HideAllWindows();
    }

    public void ToggleSettings()
    {
        settingsWindowIsActive = !settingsWindowIsActive;
        Settings.SetActive(settingsWindowIsActive);
    }
    public void ToggleCreddits()
    {
        credditsWindowIsActive = !credditsWindowIsActive;
        Creddits.SetActive(credditsWindowIsActive);
    }
    public void ToggleControls()
    {
        controlsWindowIsActive = !controlsWindowIsActive;
        Controls.SetActive(controlsWindowIsActive);
    }

    public void ShowGameOverWindow()
    {
        GameOverWindow.SetActive(true);
    }
    public void ShowPlayButton()
    {
        playButton.SetActive(true);
    }

    public void HidePlayButton()
    {
        playButton.SetActive(false);
    }

    public void HideAllWindows()
    {
        settingsWindowIsActive = false;
        credditsWindowIsActive = false;
        controlsWindowIsActive = false;
        Settings.SetActive(settingsWindowIsActive);
        Creddits.SetActive(settingsWindowIsActive);
        Controls.SetActive(settingsWindowIsActive);
    }
}
