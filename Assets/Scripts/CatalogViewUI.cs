using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatalogViewUI : MonoBehaviour
{

    public CatalogItemList itemList;
    public GameObject layoutGroup;
    public GameObject itemGroup;


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
}
