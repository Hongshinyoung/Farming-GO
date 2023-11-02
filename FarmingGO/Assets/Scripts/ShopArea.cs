using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopArea : MonoBehaviour
{
    public GameObject shopOpen;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shopOpen.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shopOpen.SetActive(false);
        }
    }
}
