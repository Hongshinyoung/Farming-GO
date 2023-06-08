using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToInven : MonoBehaviour
{
    public Sprite eventItemImage;

}
//public InventoryManager inventoryManager; // InventoryManager 인스턴스를 참조할 변수
//public Sprite eventItem;
//private void Start()
//{
//    // InventoryManager 인스턴스 가져오기
//    inventoryManager = FindObjectOfType<InventoryManager>();
//}

//private void AddItemToInventory()
//{
//    // 원하는 위치에 스프라이트 추가
//    int slotIndex = 0; // 슬롯 인덱스
//    ItemSlotData itemSlot = inventoryManager.itemSlots[slotIndex];
//    itemSlot.itemData = ScriptableObject.CreateInstance<ItemData>(); // ItemData 생성자에 스프라이트 전달
//    itemSlot.quantity = 1; // 아이템 수량 설정
//}