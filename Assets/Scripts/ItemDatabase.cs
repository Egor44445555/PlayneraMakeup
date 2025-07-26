using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase main;
    [SerializeField] List<ItemData> Lipstick = new List<ItemData>();
    [SerializeField] List<ItemData> Blush = new List<ItemData>();
    [SerializeField] List<ItemData> Colors = new List<ItemData>();

    void Awake()
    {
        main = this;
    }

    public List<ItemData> GetItemsData(string type)
    {
        if (type == "Lips")
        {
            return Lipstick;
        }
        else if (type == "Blush")
        {
            return Blush;
        }
        else if (type == "Colors")
        {
            return Colors;
        }
        else
        {
            return null;
        }
    }

    public ItemData GetItemData(string type, string nameItem)
    {
        List<ItemData> array = new List<ItemData>();

        if (type == "Lips")
        {
            array = Lipstick;
        }
        else if (type == "Blush")
        {
            array = Blush;
        }
        else if (type == "Colors")
        {
            array = Colors;
        }

        return array.Find(item => item.itemName == nameItem);
    }
}
