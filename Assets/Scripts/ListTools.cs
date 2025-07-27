using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ListTools : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] string nameList = "";

    void Start()
    {
        foreach (ItemData item in ItemDatabase.main.GetItemsData(nameList))
        {
            GameObject newObject = Instantiate(prefab, transform);
            newObject.GetComponent<Image>().sprite = item.icon;
            newObject.GetComponent<Item>().type = item.type;
            newObject.GetComponent<Item>().itemName = item.itemName;
        }
    }
}
