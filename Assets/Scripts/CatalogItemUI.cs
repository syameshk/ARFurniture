using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatalogItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    CatalogItemSelect select;
    [SerializeField]
    private CatalogItem item;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        OnClick();
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
        if (select != null && this.item != null)
            select.Selected = item;
        else
            Debug.LogWarning("Could not set selected Item");

        SceneHelper helper = FindObjectOfType<SceneHelper>();
        if (helper != null)
            helper.LoadARView();
    }

    public void Init(CatalogItem item)
    {
        this.item = item;
    }
}
