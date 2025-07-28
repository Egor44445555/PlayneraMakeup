using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase main;
    [SerializeField] List<ItemData> Lipstick = new List<ItemData>();
    [SerializeField] List<ItemData> Blush = new List<ItemData>();
    [SerializeField] List<ItemData> Shadows = new List<ItemData>();

    void Awake()
    {
        main = this;
    }

    public List<ItemData> GetItemsData(ItemType type)
    {
        if (type == ItemType.Lips)
        {
            return Lipstick;
        }
        else if (type == ItemType.Blush)
        {
            return Blush;
        }
        else if (type == ItemType.Shadows)
        {
            return Shadows;
        }
        else
        {
            return null;
        }
    }

    public ItemData GetItemData(ItemType type, string nameItem)
    {
        List<ItemData> array = new List<ItemData>();

        if (type == ItemType.Lips)
        {
            array = Lipstick;
        }
        else if (type == ItemType.Blush)
        {
            array = Blush;
        }
        else if (type == ItemType.Shadows)
        {
            array = Shadows;
        }

        return array.Find(item => item.itemName == nameItem);
    }
}
