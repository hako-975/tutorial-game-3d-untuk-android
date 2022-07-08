using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Items[] items;
    public GameObject itemSlotPrefabs;

    GameObject[] itemSlot;

    public int maxItem = 6;
    bool isDropping = false;

    // Start is called before the first frame update
    void Start()
    {
        itemSlot = new GameObject[maxItem];

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i] = Instantiate(itemSlotPrefabs, Vector3.zero, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxItem; i++)
        {
            if (i > PlayerPrefsManager.instance.GetInventory() - 1)
            {
                Image iconItemSlot = itemSlot[i].transform.GetChild(0).GetComponent<Image>();
                iconItemSlot.color = new Color(1, 1, 1, 0);
                iconItemSlot.sprite = null;

                itemSlot[i].GetComponent<Button>().enabled = false;
                itemSlot[i].transform.GetChild(1).gameObject.SetActive(false);
                itemSlot[i].transform.GetChild(2).gameObject.SetActive(false);
                itemSlot[i].transform.GetChild(2).transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                for (int j = 0; j < items.Length; j++)
                {
                    if (items[j].keyItem == PlayerPrefsManager.instance.GetItemKeyIndex(i))
                    {
                        string itemKey = items[j].keyItem;
                        int slotTo = i;

                        itemSlot[i].name = items[j].keyItem;

                        Image iconItemSlot = itemSlot[i].transform.GetChild(0).GetComponent<Image>();
                        iconItemSlot.color = Color.white;
                        iconItemSlot.sprite = items[j].iconItem;

                        itemSlot[i].GetComponent<Button>().enabled = true;
                        itemSlot[i].transform.GetChild(1).gameObject.SetActive(true);
                        itemSlot[i].GetComponentInChildren<TextMeshProUGUI>().text = items[j].nameItem;

                        if (itemSlot[i].transform.GetChild(2).gameObject.activeSelf)
                        {
                            // description panel
                            itemSlot[i].transform.GetChild(2).transform.GetChild(1).transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = items[j].descriptionItem;

                            // drop button
                            itemSlot[i].transform.GetChild(2).transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() =>
                            {
                                if (isDropping == false)
                                {
                                    isDropping = true;

                                    for (int k = 0; k < items.Length; k++)
                                    {
                                        if (itemSlot[slotTo].name == items[k].keyItem)
                                        {
                                            // last data
                                            if (slotTo == PlayerPrefsManager.instance.GetInventory() - 1)
                                            {
                                                PlayerPrefsManager.instance.DeleteItemKeyIndex(slotTo);
                                            }
                                            else
                                            {
                                                // delete current item
                                                PlayerPrefsManager.instance.DeleteItemKeyIndex(slotTo);

                                                // get next itemKey
                                                for (int l = slotTo + 1; l < itemSlot.Length; l++)
                                                {
                                                    for (int m = 0; m < items.Length; m++)
                                                    {
                                                        if (items[m].keyItem == itemSlot[l].name)
                                                        {
                                                            PlayerPrefsManager.instance.SetItemKeyIndex(items[m].keyItem, l - 1);
                                                        }
                                                    }
                                                }
                                            }

                                            // decrease stack
                                            PlayerPrefsManager.instance.SetInventory(PlayerPrefsManager.instance.GetInventory() - 1);

                                            // close panel
                                            itemSlot[slotTo].transform.GetChild(2).gameObject.SetActive(false);
                                        }
                                    }
                                }
                            });

                            isDropping = false;
                        }
                    }
                }
            }
        }
    }
}
