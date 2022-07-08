using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] spawnPointObj = GameObject.FindGameObjectsWithTag("SpawnPoint");

        if (spawnPointObj.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
