using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string description;
    public string equipTool;
    public Sprite thumbnail;
    public string itemName;

    public GameObject gameModel;

    public int cost;

    //public override bool Equals(object obj)
    //{
    //    if (obj == null || GetType() != obj.GetType())
    //    {
    //        return false;
    //    }

    //    ItemData otherItem = (ItemData)obj;
    //    return itemID == otherItem.itemID; // 아이템 ID로 비교
    //}

    //public override int GetHashCode()
    //{
    //    return base.GetHashCode();
    //}
}
