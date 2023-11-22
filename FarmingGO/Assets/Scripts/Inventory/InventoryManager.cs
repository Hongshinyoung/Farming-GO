using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        //If there is more than one instance, destroy the extra
        if (Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this && Instance != null)
        {
            Destroy(this);
        }   
        DontDestroyOnLoad(this.gameObject);
    }

    public ItemIndex itemIndex;

    [Header("Tools")]
    //Tool Slots
    [SerializeField]
    private ItemSlotData[] toolSlots = new ItemSlotData[20];
    //Tool in the player's hand
    [SerializeField]
    private ItemSlotData equippedToolSlot = null;
    public Transform handPoint2;

    [Header("Items")]
    //Item Slots
    [SerializeField]
    private ItemSlotData[] itemSlots = new ItemSlotData[20];
    //Item in the player's hand
    [SerializeField]
    private ItemSlotData equippedItemSlot = null;

    //The transform for the player to hold items in the scene
    public Transform handPoint;

    //Equipping

    //Handles movement of item from Inventory to Hand
    public void InventoryToHand(int slotIndex, InventorySlot.InventoryType inventoryType)
    {
        //The slot to equip (Tool by default)
        ItemSlotData handToEquip = equippedToolSlot;
        //The array to change
        ItemSlotData[] inventoryToAlter = toolSlots;

        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            //Change the slot to item
            handToEquip = equippedItemSlot;
            inventoryToAlter = itemSlots;
        }

        //Check if stackable
        if (handToEquip.Stackable(inventoryToAlter[slotIndex]))
        {
            ItemSlotData slotToAlter = inventoryToAlter[slotIndex];

            //Add to the hand slot
            handToEquip.AddQuantity(slotToAlter.quantity);

            //Empty the inventory slot
            slotToAlter.Empty();


        }
        else
        {
            //Not stackable
            //Cache the Inventory ItemSlotData
            ItemSlotData slotToEquip = new ItemSlotData(inventoryToAlter[slotIndex]);

            //Change the inventory slot to the hands
            inventoryToAlter[slotIndex] = new ItemSlotData(handToEquip);

            EquipHandSlot(slotToEquip);
        }

        //Update the changes in the scene
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            RenderHand();
        }
        if (inventoryType == InventorySlot.InventoryType.Tool)
        {
            RenderHand();
        }

        //Update the changes to the UI
        UIManager.Instance.RenderInventory();

    }

    //Handles movement of item from Hand to Inventory
    public void HandToInventory(InventorySlot.InventoryType inventoryType)
    {
        //The slot to move from (Tool by default)
        ItemSlotData handSlot = equippedToolSlot;
        //The array to change
        ItemSlotData[] inventoryToAlter = toolSlots;

        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            handSlot = equippedItemSlot;
            inventoryToAlter = itemSlots;
        }

        //Try stacking the hand slot. 
        //Check if the operation failed
        if (!StackItemToInventory(handSlot, inventoryToAlter))
        {
            //Find an empty slot to put the item in 
            //Iterate through each inventory slot and find an empty slot
            for (int i = 0; i < inventoryToAlter.Length; i++)
            {
                if (inventoryToAlter[i].IsEmpty())
                {
                    //Send the equipped item over to its new slot
                    inventoryToAlter[i] = new ItemSlotData(handSlot);
                    //Remove the item from the hand
                    handSlot.Empty();
                    break;
                }
            }

        }

        //Update the changes in the scene
        if (inventoryType == InventorySlot.InventoryType.Item && inventoryType == InventorySlot.InventoryType.Tool)
        {
            RenderHand();
        }

        //Update the changes to the UI
        UIManager.Instance.RenderInventory();


    }


    //Iterate through each of the items in the inventory to see if it can be stacked
    //Will perform the operation if found, returns false if unsuccessful
    public bool StackItemToInventory(ItemSlotData itemSlot, ItemSlotData[] inventoryArray)
    {

        for (int i = 0; i < inventoryArray.Length; i++)
        {
            if (inventoryArray[i].Stackable(itemSlot))
            {
                //Add to the inventory slot's stack
                inventoryArray[i].AddQuantity(itemSlot.quantity);
                //Empty the item slot
                itemSlot.Empty();
                return true;
            }
        }

        //Can't find any slot that can be stacked
        return false;
    }

    public void ShopToInventory(ItemSlotData itemSlotToMove)
    {

        ItemSlotData[] inventoryToAlter = IsTool(itemSlotToMove.itemData) ? toolSlots : itemSlots;

        //Try stacking the hand slot. 
        //Check if the operation failed
        if (!StackItemToInventory(itemSlotToMove, inventoryToAlter))
        {
            //Find an empty slot to put the item in 
            //Iterate through each inventory slot and find an empty slot
            for (int i = 0; i < inventoryToAlter.Length; i++)
            {
                if (inventoryToAlter[i].IsEmpty())
                {
                    //Send the equipped item over to its new slot
                    inventoryToAlter[i] = new ItemSlotData(itemSlotToMove);
                    break;
                }
            }

        }

        //Update the changes to the UI
        UIManager.Instance.RenderInventory();
        RenderHand();
    }

    //Render the player's equipped item in the scene
    public void RenderHand()
    {
        // Reset objects on the hand
        if (handPoint.childCount > 0)
        {
            Destroy(handPoint.GetChild(0).gameObject);
        }

        // Check if the player has any item or tool equipped
        if (SlotEquipped(InventorySlot.InventoryType.Item))
        {
            // Instantiate the game model of the equipped item and put it on the scene
            Instantiate(GetEquippedSlotItem(InventorySlot.InventoryType.Item).gameModel, handPoint);
        }

        if (SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            // Instantiate the game model of the equipped tool and put it on the scene
            Instantiate(GetEuippedSlotTool(InventorySlot.InventoryType.Tool).gameModel, handPoint);
        }
    }


    //Inventory Slot Data 
    #region Gets and Checks
    //Get the slot item (ItemData) 
    public ItemData GetEquippedSlotItem(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlot.itemData;
        }
        return equippedToolSlot.itemData;
    }

    public ItemData GetEuippedSlotTool(InventorySlot.InventoryType inventoryType)
    {
        if(inventoryType == InventorySlot.InventoryType.Tool)
        {
            return equippedToolSlot.itemData;
        }
        return equippedToolSlot.itemData;
    }

    //Get function for the slots (ItemSlotData)
    public ItemSlotData GetEquippedSlot(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            return equippedItemSlot;
        }
        return equippedToolSlot;
    }

    public ItemSlotData[] GetInventorySlots(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            return itemSlots;
        }
        return toolSlots;
    }

    public bool SlotEquipped(InventorySlot.InventoryType inventoryType)
    {
        if (inventoryType == InventorySlot.InventoryType.Item)
        {
            return !equippedItemSlot.IsEmpty();
        }
        return !equippedToolSlot.IsEmpty();
    }

    public bool IsTool(ItemData item)
    {
        //Is it equipment? 
        //Try to cast it as equipment first
        EquipmentData equipment = item as EquipmentData;
        if (equipment != null)
        {
            return true;
        }

        //Is it a seed?
        //Try to cast it as a seed
        SeedData seed = item as SeedData;
        //If the seed is not null it is a seed 
        return seed != null;

    }

    #endregion

    public void EquipHandSlot(ItemData item)
    {
        if (IsTool(item))
        {
            equippedToolSlot = new ItemSlotData(item);
        }
        else
        {
            equippedItemSlot = new ItemSlotData(item);
        }

    }

    public void EquipHandSlot(ItemSlotData itemSlot)
    {
        //Get the item data from the slot 
        ItemData item = itemSlot.itemData;

        if (IsTool(item))
        {
            equippedToolSlot = new ItemSlotData(itemSlot);
        }
        else
        {
            equippedItemSlot = new ItemSlotData(itemSlot);
        }
    }

    public void ConsumeItem(ItemSlotData itemSlot)
    {
        if (itemSlot.IsEmpty())
        {
            Debug.LogError("There is nothing to consume!");
            return;
        }

        //Use up one of the item slots
        itemSlot.Remove();
        //Refresh inventory
        RenderHand();
        UIManager.Instance.RenderInventory();
    }


    #region Inventory Slot Validation
    private void OnValidate()
    {
        //Validate the hand slots
        ValidateInventorySlot(equippedToolSlot);
        ValidateInventorySlot(equippedItemSlot);

        ValidateInventorySlots(itemSlots);
        ValidateInventorySlots(toolSlots);

    }

    //When giving the itemData value in the inspector, automatically set the quantity to 1 
    void ValidateInventorySlot(ItemSlotData slot)
    {
        if (slot.itemData != null && slot.quantity == 0)
        {
            slot.quantity = 1;
        }
    }

    void ValidateInventorySlots(ItemSlotData[] array)
    {
        foreach (ItemSlotData slot in array)
        {
            ValidateInventorySlot(slot);
        }
    }
    #endregion

    //Save파트

    // SaveData 클래스로의 변환
    //public  SaveData GetSaveData()
    //{
    //    SaveData saveData = new SaveData();
    //    saveData.toolSlotInfo = ConvertItemSlotDataToStringArray(toolSlots);
    //    saveData.itemSlotInfo = ConvertItemSlotDataToStringArray(itemSlots);
    //    Debug.Log("저장");
    //    return saveData;
    //}

    //// 역변환
    //public void LoadSaveData(SaveData saveData)
    //{
    //    ConvertStringArrayToItemSlotData(saveData.toolSlotInfo, toolSlots);
    //    ConvertStringArrayToItemSlotData(saveData.itemSlotInfo, itemSlots);
    //    Debug.Log("로드");
    //}

    //// ItemSlotData 배열을 string 배열로 변환
    //private string[] ConvertItemSlotDataToStringArray(ItemSlotData[] slots)
    //{
    //    List<string> slotInfoList = new List<string>();
    //    foreach (ItemSlotData slot in slots)
    //    {
    //        // 각 슬롯의 정보를 문자열로 변환하여 리스트에 추가
    //        string slotInfo = JsonUtility.ToJson(slot);
    //        slotInfoList.Add(slotInfo);
    //    }
    //    return slotInfoList.ToArray();
    //}

    //// string 배열을 ItemSlotData 배열로 변환
    //private void ConvertStringArrayToItemSlotData(string[] slotInfoArray, ItemSlotData[] slots)
    //{
    //    for (int i = 0; i < slotInfoArray.Length; i++)
    //    {
    //        // 문자열을 역직렬화하여 슬롯에 할당
    //        ItemSlotData slot = JsonUtility.FromJson<ItemSlotData>(slotInfoArray[i]);
    //        slots[i] = slot;
    //    }
    //}


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool HasItem(string itemName)
    {
        foreach (ItemSlotData slot in toolSlots)
        {
            if (slot.itemData != null && slot.itemData.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}