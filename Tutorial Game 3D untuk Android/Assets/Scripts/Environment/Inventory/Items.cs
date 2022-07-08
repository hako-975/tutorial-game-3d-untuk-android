using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items")]
public class Items : ScriptableObject
{
    public string keyItem;
    public string nameItem;
    public string descriptionItem;
    public Sprite iconItem;
}
