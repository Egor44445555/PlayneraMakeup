using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandController : MonoBehaviour
{
    public static HandController main;

    [Header("Settings")]
    [SerializeField] float moveSpeed = 8f;
    [SerializeField] float grabDistance = 0.1f;
    [SerializeField] Image imageHand;
    [SerializeField] Transform itemPoint;
    [SerializeField] Sprite handSprite;
    [SerializeField] Sprite handGrabSprite;
    [SerializeField] GameObject finger;
    [SerializeField] GameObject eyeBrush;
    [SerializeField] GameObject blushBrush;    

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
    Transform imageTransform;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
        mainCamera = Camera.main;
        wrapper = canvas.GetComponent<RectTransform>();
        handRectTransform = GetComponent<RectTransform>();
        targetPosition = handRectTransform.anchoredPosition;
        basedPosition = handRectTransform.anchoredPosition;
        imageTransform = imageHand.GetComponent<Transform>();
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

    public void EndTouch(Vector2 position, bool local = false)
    {
        Vector3 touch = position;        
        Vector3 touchWorldPos = mainCamera.ScreenToWorldPoint(position);

        if (!local)
        {
            touch = touchWorldPos;
        }

        if (isHolding && faceArea.OverlapPoint(touchWorldPos) && !isAnimating)
        {
            ApplyToFace();
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(touch, Vector2.zero);

        if (hit.collider != null && hit.collider.TryGetComponent<Item>(out Item item) && !isHolding)
        {
            // Apply item
            targetObject = item;

            // Start animating palette
            if (item.type == ItemType.Blush || item.type == ItemType.Shadows && blushBrush != null && eyeBrush != null)
            {
                blushBrush.SetActive(item.type == ItemType.Blush);
                eyeBrush.SetActive(item.type == ItemType.Shadows);

                Grab(false);
                AnimationsHand.main.StartAnimationBrush(targetObject);
            }
        }
    }

    public void SetTargetPosition(Vector2 position, bool local = false)
    {
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera && !local)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                handRectTransform.parent as RectTransform,
                position,
                canvas.worldCamera,
                out Vector2 localPosition
            );

            targetPosition = localPosition;
        }
        else
        {
            targetPosition = position;
        }      

        isMoving = true;
    }

    void Grab(bool portablePbject = true)
    {
        // Switching the hand state to grabbed
        isHolding = true;
        imageHand.sprite = handGrabSprite;
        finger.SetActive(true);

        if (portablePbject)
        {
            targetObject.MoveToHand();
            targetObject.GetTransform().SetParent(imageTransform);
            targetObject.GetTransform().position = itemPoint.position;
        }        
    }

    void ApplyToFace()
    {
        // Using an item on a key object
        isAnimating = true;
        AnimationsHand.main.StartAnimation(targetObject);

        if (targetObject != null && targetObject.GetParticleObject() != null)
        {
            targetObject.GetParticleObject().SetActive(true);
        }
    }

    public void ReturnHand()
    {
        // Returning the hand to the starting position
        targetPosition = basedPosition;
        isMoving = true;
        blushBrush.SetActive(false);
        eyeBrush.SetActive(false);
        isHolding = false;
        isAnimating = false;
        imageHand.sprite = handSprite;
        finger.SetActive(false);

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

    public bool IsAnimating()
    {
        return isAnimating;
    }
}