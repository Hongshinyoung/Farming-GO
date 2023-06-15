using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopListingManager : MonoBehaviour
{
    public GameObject shopLisiting;
    public Transform listingGrid;

    public void RenderShop(List<ItemData> shopItems)
    {
        if(listingGrid.childCount > 0)
        {
            foreach(Transform child in listingGrid)
            {
                Destroy(child.gameObject);
            }
        }

        foreach(ItemData shopItem in shopItems)
        {
            GameObject listingGameObject = Instantiate(shopLisiting, listingGrid);

            listingGameObject.GetComponent<ShopListing>().Display(shopItem);
        }
    }
}
