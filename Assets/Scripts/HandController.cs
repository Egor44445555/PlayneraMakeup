using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public static HandController main;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float grabDistance = 0.1f;
    RectTransform handRectTransform;

    Collider2D faceArea;
    Item targetObject;
    bool isHolding = false;
    bool isMoving = false;
    bool isAnimating = false;
    Vector2 targetPosition;
    Vector2 basedPosition;
    Camera mainCamera;
    Canvas canvas;
    Transform wrapper;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
        mainCamera = Camera.main;
        wrapper = canvas.GetComponent<RectTransform>();
        handRectTransform = GetComponent<RectTransform>();
        targetPosition = handRectTransform.anchoredPosition;
        basedPosition = handRectTransform.anchoredPosition;
    }

    void Start()
    {
        faceArea = BodyManager.main.GetArea();
    }

    void Update()
    {
        if (isMoving && !isAnimating)
        {
            handRectTransform.anchoredPosition = Vector2.Lerp(handRectTransform.anchoredPosition, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(handRectTransform.anchoredPosition, targetPosition) < 0.1f)
            {
                isMoving = false;
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
        isHolding = true;
        targetObject.MoveToHand();
    }

    void ApplyToFace()
    {
        isAnimating = true;
        BodyManager.main.ApplyingMakeup(targetObject);
        targetObject.ReturnPosition();
        targetObject = null;
        isHolding = false;
        ReturnHand();
    }

    public void ReturnHand()
    {
        targetPosition = basedPosition;
        isHolding = false;
        isMoving = true;
        isAnimating = false;

        if (targetObject != null)
        {
            targetObject.ReturnPosition();
            targetObject = null;
        }
    }

    public RectTransform GetTransform()
    {
        return handRectTransform;
    }

    public Transform GetWrapper()
    {
        return wrapper;
    }
}