using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    int currentLevel;
    GameObject spawnPoint;

    public GameObject activeCheckPoint;
    public GameObject deactiveCheckPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

        currentLevel = SceneManager.GetActiveScene().buildIndex;
        
        if (currentLevel != PlayerPrefsManager.instance.GetCurrentLevel())
        {
            // reset spawn point
            spawnPoint.transform.position = Vector3.up * 0.25f;
            // set current level
            PlayerPrefsManager.instance.SetCurrentLevel(currentLevel);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (activeCheckPoint.gameObject.activeSelf == false)
            {
                spawnPoint.transform.position = transform.position;
            }

            activeCheckPoint.gameObject.SetActive(true);
            deactiveCheckPoint.gameObject.SetActive(false);
        }
    }
}
