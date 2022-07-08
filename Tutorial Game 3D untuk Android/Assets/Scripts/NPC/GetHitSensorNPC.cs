using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitSensorNPC : MonoBehaviour
{
    public NPCController nPCController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHandPlayer") || other.CompareTag("RightHandPlayer"))
        {
            nPCController.isGetHit = true;
        }
    }
}
