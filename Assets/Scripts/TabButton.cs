using UnityEngine;
using UnityEngine.UI;

public class TabButton : MonoBehaviour
{
    [SerializeField] ItemType type;
    [SerializeField] Image image;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] Sprite activetSprite;

    public void OpenTab()
    {
        BookManager.main.OpenTab(type);
    }

    public ItemType GetType()
    {
        return type;
    }

    public void SetActiveTab()
    {
        image.sprite = activetSprite;
    }

    public void RemoveActivityTab()
    {
        image.sprite = defaultSprite;
    }
}
