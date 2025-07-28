using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager main;
    [SerializeField] GameObject LipsList;
    [SerializeField] GameObject BlushList;
    [SerializeField] GameObject ShadowsList;
    [SerializeField] RectTransform ShadowsBrush;
    [SerializeField] RectTransform BlushBrush;

    Canvas canvas;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OpenTab(ItemType type)
    {
        LipsList.SetActive(type == ItemType.Lips);
        BlushList.SetActive(type == ItemType.Blush);
        ShadowsList.SetActive(type == ItemType.Shadows);
    }
}
