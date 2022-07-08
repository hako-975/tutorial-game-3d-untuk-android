using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public void HomeButton()
    {
        PlayerPrefsManager.instance.SetLastScene("Home");
    }    

    public void SchoolButton()
    {
        PlayerPrefsManager.instance.SetLastScene("School");
    }

    public void BackButtonMap()
    {
        PlayerPrefsManager.instance.SetLastScene("Main Menu");
    }
}
