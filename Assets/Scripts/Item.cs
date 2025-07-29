using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemType type;
    public string itemName;
    public bool portablePbject = true;

    [SerializeField] float moveSpeed = 8f;    
    Vector2 basedanchoredPosition;
    Vector2 basedPosition;
    RectTransform transformObject;
    BoxCollider2D collider;
    bool returnPosition = false;
    bool moveToHand = false;
    bool itemCaptured = false;
    bool isListItem = false;
    Transform parent;

    void Awake()
    {
        transformObject = GetComponent<RectTransform>();
        collider = GetComponent<BoxCollider2D>();
        parent = transform.parent;

        SaveBasePosition();

        LayoutGroup layoutGroup = parent.GetComponent<LayoutGroup>();
        
        if (layoutGroup is GridLayoutGroup gridLayout)
        {
            isListItem = true;
        }
    }

    void Update()
    {
        if (returnPosition)
        {
            transform.SetParent(parent);
            transformObject.anchoredPosition = Vector2.Lerp(transformObject.anchoredPosition, basedanchoredPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transformObject.position, basedPosition) < 0.01f)
            {
                returnPosition = false;
                transformObject.anchoredPosition = basedanchoredPosition;
                
                if (isListItem)
                {
                    LayoutRebuilder.MarkLayoutForRebuild(transformObject);
                }
            }
        }

        if (moveToHand && portablePbject)
        {
            if (isListItem)
            {                
                transform.localScale = Vector3.one;
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

    public Transform GetParent()
    {
        return parent;
    }

    void SaveBasePosition()
    {
        basedanchoredPosition = transformObject.anchoredPosition;
        basedPosition = transformObject.position;
    }

    public void ReturnPosition()
    {
        if (isListItem)
        {
            transform.SetParent(parent);
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;
            
            Canvas.ForceUpdateCanvases();
            SaveBasePosition();
        }

        returnPosition = true;
        itemCaptured = false;
        moveToHand = false;
    }

    public void MoveToHand()
    {
        moveToHand = true;
    }
}
