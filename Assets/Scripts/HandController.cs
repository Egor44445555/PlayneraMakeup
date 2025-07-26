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
    Vector3 targetPosition;
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

            if (Vector2.Distance(handRectTransform.anchoredPosition, targetPosition) < grabDistance)
            {
                isMoving = false;
                Grab();
            }
        }
    }

    public void EndTouch(PointerEventData eventData)
    {
        if (isHolding)
        {
            Vector2 touchWorldPos = mainCamera.ScreenToWorldPoint(eventData.position);

            if (faceArea.OverlapPoint(touchWorldPos))
            {
                ApplyToFace();
            }
        }
    }

    public void SetTargetPosition(Vector2 screenPosition)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                handRectTransform.parent as RectTransform,
                screenPosition,
                canvas.worldCamera,
                out Vector2 localPosition
            );

            targetPosition = localPosition;
        }
        else
        {
            targetPosition = screenPosition;
        }
        
        isMoving = true;
    }

    public void SetTargetObject(Item item)
    {
        targetObject = item;
    }

    public void StartGrabbing()
    {
        if (targetObject != null && !isHolding && !isMoving)
        {
            isMoving = true;
            targetPosition = targetObject.GetTransform().position;
        }
    }

    void Grab()
    {
        isHolding = true;
    }

    void ApplyToFace()
    {
        isHolding = false;
    }

    public void ReturnHand()
    {
        targetPosition = basedPosition;
        isHolding = false;
        isMoving = true;
    }
}