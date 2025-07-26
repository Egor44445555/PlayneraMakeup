using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase main;
    public List<ItemData> items = new List<ItemData>();

    void Awake()
    {
        main = this;
    }

    public ItemData GetItem(string name)
    {
        return items.Find(item => item.itemName == name);
    }
}
