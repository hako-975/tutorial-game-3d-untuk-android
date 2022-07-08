using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Interact : MonoBehaviour
{
    public Dialogue dialogue;

    public GameObject interactView;
    public GameObject dialogueView;
    public GameObject interactButton;
    public GameObject combatButton;
    public GameObject healthBarNPC;

    public TextMeshProUGUI dialogueText;

    public NPCController nPCController;

    Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        interactView.SetActive(false);
        dialogueView.SetActive(false);
        interactButton.SetActive(false);
        combatButton.SetActive(false);
        healthBarNPC.SetActive(false);

        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nPCController.isInteract)
        {
            interactView.SetActive(false);
            dialogueView.SetActive(true);
        }
        else
        {
            dialogueView.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactView.SetActive(true);
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactView.SetActive(false);
            interactButton.SetActive(false);

            nPCController.isInteract = false;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // jika kalimat habis
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            // durasi menampilkan huruf-huruf
            yield return new WaitForSeconds(0.1f);
        }

        // durasi untuk jeda ganti kalimat
        yield return new WaitForSeconds(0.5f);

        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        nPCController.isInteract = false;
    }

    public void DialogueTrigger()
    {
        combatButton.SetActive(false);
        healthBarNPC.SetActive(false);
        StartDialogue(dialogue);
        nPCController.Interact();
    }
}
