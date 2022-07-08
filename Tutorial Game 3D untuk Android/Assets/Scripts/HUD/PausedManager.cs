using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedManager : MonoBehaviour
{
    public GameObject joystick;
    public GameObject pausedButton;
    public GameObject pausedMenu;
    public GameObject settingsPanel;
    public GameObject healthBar;
    public GameObject inventoryPanel;
    public GameObject dyingPanel;

    public GameObject[] otherObject;

    PlayerStats playerStats;

    private void Start()
    {
        healthBar.SetActive(true);
        inventoryPanel.SetActive(true);
        dyingPanel.SetActive(false);
        playerStats = FindObjectOfType<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats.isDying)
        {
            dyingPanel.SetActive(true);

            joystick.SetActive(false);
            pausedButton.SetActive(false);
            
            settingsPanel.SetActive(false);

            for (int i = 0; i < otherObject.Length; i++)
            {
                otherObject[i].SetActive(false);
            }
        }
    }

    public void Paused()
    {
        Time.timeScale = 0;
        pausedButton.SetActive(false);
        joystick.SetActive(false);
        settingsPanel.SetActive(false);
        healthBar.SetActive(false);
        inventoryPanel.SetActive(false);

        for (int i = 0; i < otherObject.Length; i++)
        {
            otherObject[i].SetActive(false);
        }

        pausedMenu.SetActive(true);
    }

    public void Resume()
    {
        joystick.SetActive(true);
        pausedButton.SetActive(true);
        healthBar.SetActive(true);
        inventoryPanel.SetActive(true);

        for (int i = 0; i < otherObject.Length; i++)
        {
            otherObject[i].SetActive(true);
        }

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

    public void MainMenu()
    {
        Time.timeScale = 1;
        // melakukan reset spawn point
        PlayerPrefsManager.instance.SetCurrentLevel(0);
        PlayerPrefsManager.instance.SetLastScene("Main Menu");
    }

}
