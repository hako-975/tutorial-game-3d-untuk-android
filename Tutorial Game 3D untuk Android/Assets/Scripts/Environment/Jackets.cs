using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Jacket", menuName = "Jackets")]
public class Jackets : ScriptableObject
{
    public string nameJacket;
    public Material materialJacket;
    public Sprite imageJacket;
}
