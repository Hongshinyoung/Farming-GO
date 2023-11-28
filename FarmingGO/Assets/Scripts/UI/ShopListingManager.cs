using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopListingManager : MonoBehaviour
{
    public GameObject shopLisiting;
    public Transform listingGrid;

    ItemData itemToBuy;
    int quantity;

    [Header("Confirmation Screen")]
    public GameObject confirmationScreen;
    public Text confirmationPrompt;
    public Text quantityText;
    public Text costCalculationText;
    public Button purchaseButton;

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

    public void OpenConfirmationScreen(ItemData item)
    {
        itemToBuy = item;
        quantity = 1;
        RenderConfirmationScreen();
    }

    public void RenderConfirmationScreen()
    {
        confirmationScreen.SetActive(true);

        confirmationPrompt.text = $"{itemToBuy.name}을 구매하시겠습니까?";

        quantityText.text = "x" + quantity;

        int cost = itemToBuy.cost * quantity;

        int playerMoneyLeft = PlayerStats.Money - cost;

        if(playerMoneyLeft < 0)
        {
            costCalculationText.text = "골드가 부족합니다";
            purchaseButton.interactable = false;
            return;
        }

        purchaseButton.interactable = true;

        costCalculationText.text = $"{PlayerStats.Money} > {playerMoneyLeft}";
    }

    public void AddQuantity()
    {
        quantity++;
        RenderConfirmationScreen();
    }

    public void SubtractQuantity()
    {
        if(quantity > 1)
        {
            quantity--;
        }
        RenderConfirmationScreen();
    }

    public void ConfirmPurchase()
    {
        Shop.Purchase(itemToBuy, quantity);
        confirmationScreen.SetActive(false);
    }

    public void CancelPurchase()
    {
        confirmationScreen.SetActive(false);
    }
}
