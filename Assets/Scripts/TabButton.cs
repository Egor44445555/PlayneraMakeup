using UnityEngine;

public class TabButton : MonoBehaviour
{
    [SerializeField] ItemType type;

    public void OpenTab()
    {
        BookManager.main.OpenTab(type);
    }
}
