using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatalogItemViewUI : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI subtitle;
    public TextMeshProUGUI description;
    public TextMeshProUGUI price;
    public Image icon;

    [SerializeField]
    CatalogItemSelect select;

    private void Start()
    {
        
    }
    public void Init()
    {
        this.title.text = select.Selected.Title;
        this.subtitle.text = select.Selected.ShortDescription;
        this.description.text = select.Selected.Description;
        this.price.text = string.Format("Rs {0}",select.Selected.Cost);
        this.icon.sprite = select.Selected.Images[0];
    }

    public void OnARButtonClicked()
    {
        SceneHelper helper = FindObjectOfType<SceneHelper>();
        if (helper != null)
            helper.LoadARView();
    }

    
}
