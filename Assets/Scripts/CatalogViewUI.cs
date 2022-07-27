using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogViewUI : MonoBehaviour
{

    public CatalogItemList itemList;
    public GameObject layoutGroup;
    public GameObject itemGroup;
    [SerializeField]
    CatalogItemSelect select;


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    private void Init()
    {
        for (int i = 0; i < itemList.items.Count; i++)
        {
            //Created a copy
            GameObject item = Instantiate<GameObject>(itemGroup, layoutGroup.transform);
            item.SetActive(true);

            //Set data
            CatalogItemUI itemUI = item.GetComponent<CatalogItemUI>();
            itemUI.Init(itemList.items[i]);
        }
    }

    public void OnItemSelected(CatalogItem selected)
    {
        Debug.Log("Selected : "+selected.Name);

        //Set current selected item deatils to a variable
        if (select != null)
            select.Selected = selected;
        else
            Debug.LogWarning("Could not set selected Item");
        //hide list UI
        this.gameObject.SetActive(false);
        //Show Item UI
        CatalogItemViewUI catalogItemViewUI = FindObjectOfType<CatalogItemViewUI>(true);
        if (catalogItemViewUI != null)
        {
            catalogItemViewUI.gameObject.SetActive(true);
            catalogItemViewUI.Init();
        }
    }
}
