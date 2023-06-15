using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopListing : MonoBehaviour, IPointerClickHandler
{
    public Image itemThumbnail;
    public Text nameText;
    public Text costText;

    ItemData itemData;

    public void Display(ItemData itemData)
    {
        this.itemData = itemData;
        itemThumbnail.sprite = itemData.thumbnail;
        nameText.text = itemData.name;
        costText.text = itemData.cost + PlayerStats.CURRENCY ;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Instance.shopListingManager.OpenConfirmationScreen(itemData);
    }

}
