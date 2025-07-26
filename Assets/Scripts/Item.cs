using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour
{
    public string nameItem;
    [SerializeField] float moveSpeed = 8f;
    Vector2 basedPosition;
    RectTransform transformObject;
    BoxCollider2D collider;
    bool returnPosition = false;

    void Awake()
    {
        transformObject = GetComponent<RectTransform>();
        collider = GetComponent<BoxCollider2D>();
        basedPosition = transformObject.anchoredPosition;
    }

    void Update()
    {
        if (returnPosition)
        {
            transformObject.anchoredPosition = Vector2.Lerp(transformObject.anchoredPosition, basedPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transformObject.anchoredPosition, basedPosition) < 0.1f)
            {
                returnPosition = false;
            }
        }
    }

    public Vector2 GetBasedPosition()
    {
        return basedPosition;
    }

    public RectTransform GetTransform()
    {
        return transformObject;
    }

    public void ReturnPosition()
    {
        returnPosition = true;
    }
}
