using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CatalogItem", menuName = "ScriptableObjects/CatalogItem", order = 1)]
public class CatalogItem : ScriptableObject
{
    public string Name;
    public string Title;
    public string ShortDescription;
    [Multiline]
    public string Description;
    public float Cost;
    public Sprite[] Images;
    public GameObject Model;
}
