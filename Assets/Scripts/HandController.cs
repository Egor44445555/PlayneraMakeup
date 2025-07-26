using UnityEngine;
using UnityEngine.EventSystems;

public class HandController : MonoBehaviour
{
    public static HandController main;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float grabDistance = 0.1f;
    [SerializeField] Collider2D faceArea;
    RectTransform handRectTransform;

    Item targetObject;
    bool isHolding = false;
    bool isMoving = false;
    Vector2 targetPosition;
    Vector2 basedPosition;
    Camera mainCamera;
    Canvas canvas;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
        mainCamera = Camera.main;
        handRectTransform = GetComponent<RectTransform>();
        targetPosition = handRectTransform.anchoredPosition;
        basedPosition = handRectTransform.anchoredPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            handRectTransform.anchoredPosition = Vector2.Lerp(handRectTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(handRectTransform.anchoredPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
            }

            if (targetObject != null && isHolding)
            {
                targetObject.GetTransform().position = handRectTransform.position;
            }

            if (targetObject != null && Vector2.Distance(handRectTransform.position, targetObject.GetTransform().position) < grabDistance && !isHolding)
            {
                Grab();
            }
        }
    }

    public void EndTouch(PointerEventData eventData)
    {
        Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);
    
        if (isHolding && faceArea.OverlapPoint(touchWorldPos))
        {
            ApplyToFace();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(touchWorldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent<Item>(out Item item))
        {
            targetObject = item;
        }
    }

    public void SetTargetPosition(PointerEventData eventData)
    {        
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                handRectTransform.parent as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPosition
            );

            targetPosition = localPosition;
        }
        else
        {
            targetPosition = eventData.position;
        }

        isMoving = true;
    }

    void Grab()
    {
        print("Grab");
        isHolding = true;
    }

    void ApplyToFace()
    {
        print("ApplyToFace");
        targetObject.ReturnPosition();
        targetObject = null;
        isHolding = false;
    }

    public void ReturnHand()
    {
        targetPosition = basedPosition;
        isHolding = false;
        isMoving = true;
    }
}