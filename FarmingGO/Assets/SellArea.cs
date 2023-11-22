using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellArea : MonoBehaviour
{
    public Button enterSellZone;
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enterSellZone.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enterSellZone.gameObject.SetActive(false);
        }
    }
}
