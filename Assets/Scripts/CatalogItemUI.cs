using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class CatalogItemUI : MonoBehaviour, IPointerClickHandler
{
    
    [SerializeField]
    private CatalogItem item;

    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image icon;

    void Start()
    {
        //Init(item);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        OnClick();
    }

    public void OnClick()
    {
        //Show Item Deatils
        CatalogViewUI catalogViewUI = GetComponentInParent<CatalogViewUI>(true);
        if(catalogViewUI != null)
        {
            catalogViewUI.OnItemSelected(item);
        }
        else
        {
            Debug.LogWarning("Could not find CatalogViewUI");
        }
    }

    public void Init(CatalogItem item)
    {
        this.item = item;
        this.title.text = item.Title;
        this.description.text = item.ShortDescription;
        this.icon.sprite = item.Images[0];

    }
}
