using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour
{
    #region Singleton
    public static PlayerPrefsManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public string GetNickName()
    {
        return PlayerPrefs.GetString("NickName", "NickName");
    }

    public string SetNickName(string nickName)
    {
        PlayerPrefs.SetString("NickName", nickName);
        return GetNickName();
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    public int SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt("CurrentLevel", level);
        return GetCurrentLevel();
    }


    public int GetInventory()
    {
        return PlayerPrefs.GetInt("Inventory", 0);
    }

    public int SetInventory(int index)
    {
        PlayerPrefs.SetInt("Inventory", index);
        return GetInventory();
    }

    public string GetItemKeyIndex(int indexSlot)
    {
        return PlayerPrefs.GetString("ItemKey" + indexSlot);
    }

    public string SetItemKeyIndex(string itemKey, int indexSlot)
    {
        PlayerPrefs.SetString("ItemKey" + indexSlot, itemKey);
        return GetItemKeyIndex(indexSlot);
    }

    public void DeleteItemKeyIndex(int indexSlot)
    {
        PlayerPrefs.DeleteKey("ItemKey" + indexSlot);
    }

    public int GetItemGroundIdBool(int id)
    {
        return PlayerPrefs.GetInt("ItemGroundIdBool" + id);
    }

    public int SetItemGroundIdBool(int id, int boolean)
    {
        PlayerPrefs.SetInt("ItemGroundIdBool" + id, boolean);
        return GetItemGroundIdBool(id);
    }

    public string GetCurrentJacket()
    {
        return PlayerPrefs.GetString("CurrentJacket", "Mat - Original");
    }

    public string SetCurrentJacket(string jacket)
    {
        PlayerPrefs.SetString("CurrentJacket", jacket);
        return GetCurrentJacket();
    }

    public int GetHealth()
    {
        return PlayerPrefs.GetInt("Health", 100);
    }

    public int SetHealth(int health)
    {
        PlayerPrefs.SetInt("Health", health);
        return GetHealth();
    }

    public float GetCameraZoom()
    {
        return PlayerPrefs.GetFloat("CameraZoom", 40f);
    }

    public float SetCameraZoom(float cameraZoom)
    {
        PlayerPrefs.SetFloat("CameraZoom", cameraZoom);
        return GetCameraZoom();
    }

    public float GetSensitivity()
    {
        return PlayerPrefs.GetFloat("Sensitivity", 60f);
    }

    public float SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        return GetSensitivity();
    }

    public string GetLastScene()
    {
        return PlayerPrefs.GetString("LastScene", "Main Menu");
    }

    public void SetLastScene(string nameScene)
    {
        PlayerPrefs.SetString("LastScene", nameScene);
        SceneManager.LoadScene("Loading");
    }

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
    }
}
