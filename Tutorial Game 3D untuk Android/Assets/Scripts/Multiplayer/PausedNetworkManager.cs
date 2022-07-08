using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedNetworkManager : MonoBehaviour
{
    public GameObject joystick;
    public GameObject pausedButton;
    public GameObject pausedMenu;
    public GameObject settingsPanel;

    private void Start()
    {
        pausedMenu.SetActive(false);
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pausedButton.SetActive(false);
        joystick.SetActive(false);
        settingsPanel.SetActive(false);
        
        pausedMenu.SetActive(true);
    }

    public void Resume()
    {
        joystick.SetActive(true);
        pausedButton.SetActive(true);

        pausedMenu.SetActive(false);
        settingsPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        // melakukan reset spawn point
        PlayerPrefsManager.instance.SetCurrentLevel(0);
        PlayerPrefsManager.instance.SetLastScene(SceneManager.GetActiveScene().name);
    }

    public void Settings()
    {
        Time.timeScale = 0;
        pausedButton.SetActive(false);
        joystick.SetActive(false);
        pausedMenu.SetActive(false);

        settingsPanel.SetActive(true);
    }

    public void BackButtonSettings()
    {
        Time.timeScale = 0;
        pausedButton.SetActive(false);
        joystick.SetActive(false);
        settingsPanel.SetActive(false);

        pausedMenu.SetActive(true);
    }
}
