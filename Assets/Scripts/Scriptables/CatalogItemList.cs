using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CatalogItemList", order = 1)]
public class CatalogItemList : ScriptableObject
{
    public List<CatalogItem> items = new List<CatalogItem>();
}
