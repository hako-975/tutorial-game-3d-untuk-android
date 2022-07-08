using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGround : MonoBehaviour
{
    public int itemGroundId;
    public Items item;

    Inventory inventory;
    
    int maxItem;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        maxItem = inventory.maxItem;

        // jika item sudah pernah diambil, lalu player melakukan restart atau quit game, hapus item
        if (PlayerPrefsManager.instance.GetItemGroundIdBool(itemGroundId) > 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int currentItemIndex = PlayerPrefsManager.instance.GetInventory();

            if (currentItemIndex < maxItem)
            {
                PlayerPrefsManager.instance.SetInventory(currentItemIndex + 1);
                PlayerPrefsManager.instance.SetItemKeyIndex(item.keyItem, currentItemIndex);
                PlayerPrefsManager.instance.SetItemGroundIdBool(itemGroundId, 1);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory Sudah Penuh");
            }
        }
    }
}
