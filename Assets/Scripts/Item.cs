using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public string nameItem;

    Vector2 basedPosition;
    Transform transformObject;

    void Awake()
    {
        basedPosition = transform.position;
        transformObject = transform;
    }

    public Vector2 GetBasedPosition()
    {
        return basedPosition;
    }

    public Transform GetTransform()
    {
        return transformObject;
    }
}
