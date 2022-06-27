using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectedItem", menuName = "ScriptableObjects/CatalogItemSelect", order = 1)]
public class CatalogItemSelect : ScriptableObject
{
    public CatalogItem Selected;
}
