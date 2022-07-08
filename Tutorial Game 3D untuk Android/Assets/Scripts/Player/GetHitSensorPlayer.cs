using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitSensorPlayer : MonoBehaviour
{
    public PlayerController playerController;

    [HideInInspector]
    public NPCStats nPCStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftHandNPC") || other.CompareTag("RightHandNPC"))
        {
            nPCStats = other.GetComponentInParent<NPCStats>();

            playerController.isGetHit = true;
        }
    }
}
