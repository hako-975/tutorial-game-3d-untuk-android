using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupBoardScript : MonoBehaviour
{
    public GameObject interactButton;
    public GameObject jacketPanel;
    public GameObject pet;

    PlayerController playerController;

    Animation anim;

    private void Start()
    {
        interactButton.SetActive(false);
        jacketPanel.SetActive(false);

        playerController = FindObjectOfType<PlayerController>();

        anim = GetComponent<Animation>();
        anim.clip.legacy = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
        }
    }

    public void OnClickInteractButton()
    {
        // remove interact button
        interactButton.SetActive(false);

        // play anim open cup board
        anim.Play("Open Cup Board");

        // force position player and make player can't move
        playerController.GetComponent<CharacterController>().enabled = false;
        playerController.transform.position = playerController.transform.position;
        playerController.transform.rotation = transform.rotation;

        // change anim to idle
        playerController.GetComponentInChildren<Animator>().Play("Base Layer.Idle");
        playerController.GetComponentInChildren<Animator>().speed = 0;

        // panel
        jacketPanel.SetActive(true);

        // remove pet
        pet.SetActive(false);
    }

    public void OnClickBackButton()
    {
        // remove interact button
        interactButton.SetActive(false);

        // play anim open cup board
        anim.Play("Close Cup Board");

        // make player can move
        playerController.GetComponent<CharacterController>().enabled = true;
        playerController.transform.position = playerController.transform.position;
        playerController.transform.rotation = transform.rotation;

        // change anim to idle
        playerController.GetComponentInChildren<Animator>().Play("Base Layer.Idle");
        playerController.GetComponentInChildren<Animator>().speed = 1;

        // panel
        jacketPanel.SetActive(false);

        // add pet
        pet.SetActive(true);
    }


}
