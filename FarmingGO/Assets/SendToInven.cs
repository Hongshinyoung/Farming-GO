using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToInven : MonoBehaviour
{
    public Sprite eventItemImage;

}
//public InventoryManager inventoryManager; // InventoryManager �ν��Ͻ��� ������ ����
//public Sprite eventItem;
//private void Start()
//{
//    // InventoryManager �ν��Ͻ� ��������
//    inventoryManager = FindObjectOfType<InventoryManager>();
//}

//private void AddItemToInventory()
//{
//    // ���ϴ� ��ġ�� ��������Ʈ �߰�
//    int slotIndex = 0; // ���� �ε���
//    ItemSlotData itemSlot = inventoryManager.itemSlots[slotIndex];
//    itemSlot.itemData = ScriptableObject.CreateInstance<ItemData>(); // ItemData �����ڿ� ��������Ʈ ����
//    itemSlot.quantity = 1; // ������ ���� ����
//}