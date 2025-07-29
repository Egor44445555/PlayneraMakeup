using UnityEngine;
using System.Collections.Generic;

public class BookManager : MonoBehaviour
{
    public static BookManager main;
    [SerializeField] GameObject LipsList;
    [SerializeField] GameObject BlushList;
    [SerializeField] GameObject ShadowsList;
    [SerializeField] RectTransform ShadowsBrush;
    [SerializeField] RectTransform BlushBrush;

    TabButton[] tabButtons;
    ItemType currentTabType;

    Canvas canvas;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
        tabButtons = FindObjectsOfType<TabButton>();
    }

    public void OpenTab(ItemType type)
    {
        currentTabType = type;
        LipsList.SetActive(type == ItemType.Lips);
        BlushList.SetActive(type == ItemType.Blush);
        ShadowsList.SetActive(type == ItemType.Shadows);

        foreach (TabButton tab in tabButtons)
        {
            if (tab.GetType() == currentTabType)
            {
                tab.SetActiveTab();
            }
            else
            {
                tab.RemoveActivityTab();
            }
        }
    }
}
