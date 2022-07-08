using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JacketsScript : MonoBehaviour
{
    public Jackets[] jackets;
    
    public GameObject imageJacket;

    public TextMeshProUGUI textJacket;

    PlayerController playerController;

    int currentIndex;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        // get and set index and current clothes
        for (int i = 0; i < jackets.Length; i++)
        {
            if (PlayerPrefsManager.instance.GetCurrentJacket() == jackets[i].nameJacket)
            {
                currentIndex = i;
            }

        }

        imageJacket.GetComponent<Image>().sprite = jackets[currentIndex].imageJacket;
        textJacket.text = jackets[currentIndex].nameJacket;
    }


    public void ButtonLeftJacket()
    {
        // check current clothes is what number index, if first change to last
        if (PlayerPrefsManager.instance.GetCurrentJacket() == jackets[0].nameJacket)
        {
            currentIndex = jackets.Length - 1;
            PlayerPrefsManager.instance.SetCurrentJacket(jackets[currentIndex].nameJacket);
        }
        else
        {
            currentIndex -= 1;
            PlayerPrefsManager.instance.SetCurrentJacket(jackets[currentIndex].nameJacket);
        }

        imageJacket.GetComponent<Image>().sprite = jackets[currentIndex].imageJacket;
        textJacket.text = jackets[currentIndex].nameJacket;

        playerController.isChangeJacket = true;
    }

    public void ButtonRightJacket()
    {
        // check current clothes is what number index, if last change to first
        if (PlayerPrefsManager.instance.GetCurrentJacket() == jackets[jackets.Length - 1].nameJacket)
        {
            currentIndex = 0;
            PlayerPrefsManager.instance.SetCurrentJacket(jackets[currentIndex].nameJacket);
        }
        else
        {
            currentIndex += 1;
            PlayerPrefsManager.instance.SetCurrentJacket(jackets[currentIndex].nameJacket);
        }

        imageJacket.GetComponent<Image>().sprite = jackets[currentIndex].imageJacket;
        textJacket.text = jackets[currentIndex].nameJacket;

        playerController.isChangeJacket = true;
    }
}
