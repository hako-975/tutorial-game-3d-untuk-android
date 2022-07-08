using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTargetScript : MonoBehaviour
{
    public NPCController nPCController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<NPCController>() == nPCController)
        {
            nPCController.isArrived = true;
        }
    }
}
