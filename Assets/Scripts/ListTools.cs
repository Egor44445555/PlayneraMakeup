using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListTools : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] ItemType type;

    void Start()
    {
        foreach (ItemData item in ItemDatabase.main.GetItemsData(type))
        {
            GameObject newObject = Instantiate(prefab, transform);
            newObject.GetComponent<Image>().sprite = item.icon;
            newObject.GetComponent<Item>().type = item.type;
            newObject.GetComponent<Item>().itemName = item.itemName;

            if (item.type == ItemType.Shadows || item.type == ItemType.Blush)
            {
                newObject.GetComponent<Item>().portablePbject = false;
            }
        }

        gameObject.SetActive(false);
    }
}
