using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public GameObject interactView;
    public GameObject dialogueView;
    public GameObject interactButton;
    public GameObject combatButton;
    public GameObject healthBarNPC;

    public NPCController nPCController;

    PlayerController playerController;

    SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        
        playerController = FindObjectOfType<PlayerController>();

        interactButton.SetActive(false);
        interactView.SetActive(false);
        dialogueView.SetActive(false);
        combatButton.SetActive(false);
        healthBarNPC.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isCombat == true)
        {
            interactButton.SetActive(false);
            interactView.SetActive(false);
            dialogueView.SetActive(false);
            healthBarNPC.SetActive(true);
            nPCController.isInteract = false;
        }
        else
        {
            healthBarNPC.SetActive(false);
        }

        // jika player pingsan
        if (PlayerPrefsManager.instance.GetHealth() <= 0)
        {
            healthBarNPC.SetActive(false);
        }

        // jika npc pingsan
        if (nPCController.isDying)
        {
            playerController.isCombat = false;
            combatButton.SetActive(false);
            interactButton.SetActive(false);
            nPCController.GetComponent<Rigidbody>().mass = 1000;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            combatButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            combatButton.SetActive(false);

            playerController.isCombat = false;
            nPCController.isCombat = false;
            sphereCollider.radius = 1;
        }
    }

    public void CombatButtonOnClick()
    {
        sphereCollider.radius = 3;

        interactButton.SetActive(false);
        interactView.SetActive(false);
        dialogueView.SetActive(false);

        playerController.isCombat = true;
        playerController.Attack();
        nPCController.Combat();
    }

}
