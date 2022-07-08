using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    public void StartButton()
    {
        if (PlayerPrefsManager.instance.GetLastScene() != "Main Menu")
        {
            PlayerPrefsManager.instance.SetLastScene(PlayerPrefsManager.instance.GetLastScene());
        }
        else
        {
            PlayerPrefsManager.instance.SetLastScene("Home");
        }
    }

    public void MapButton()
    {
        SceneManager.LoadScene("Map");
    }

    public void SettingsButton()
    {
        settingsPanel.SetActive(true);

        mainMenuPanel.SetActive(false);
    }

    public void MultiplayerButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void BackButtonSettings()
    {
        mainMenuPanel.SetActive(true);

        settingsPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
