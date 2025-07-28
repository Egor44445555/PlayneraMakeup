using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite image;
    public Sprite additionalImage;
    public Sprite icon;
    public ItemType type;
}

public enum ItemType
{
    Cream,
    Lips,
    Blush,
    Shadows,
    Loofah
}