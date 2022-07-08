using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportScript : MonoBehaviour
{
    public string teleportScene = "School";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // melakukan reset spawn point
            PlayerPrefsManager.instance.SetCurrentLevel(0);
            PlayerPrefsManager.instance.SetLastScene(teleportScene);
        }
    }
}
