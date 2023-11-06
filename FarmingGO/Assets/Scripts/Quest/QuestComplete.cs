using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using System;

public class QuestComplete : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public ItemData questTool;
    public TextMeshProUGUI questComplete;

    private bool isComplete;

    private void Update()
    {
        if(!isComplete && inventoryManager != null)
        {
            if(inventoryManager.GetEquippedSlot(InventorySlot.InventoryType.Tool).itemData == questTool)
            {
                isComplete = true;
                StartCoroutine(ActiveQuestComplete());
            }
        }
    }

    IEnumerator ActiveQuestComplete()
    {
        questComplete.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        questComplete.gameObject.SetActive(false);
    }

}
