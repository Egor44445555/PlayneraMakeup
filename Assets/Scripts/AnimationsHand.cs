using UnityEngine;
using System.Linq;

public class AnimationsHand : MonoBehaviour
{
    public static AnimationsHand main;
    [SerializeField] Animation[] animations;

    [Header("Movement Settings")]
    [SerializeField] float pointDistance = 0.5f;
    [SerializeField] float palettePointOffsetX = 0f;
    [SerializeField] float palettePointOffsetY = 0f;

    int currentIndex = 0;
    float basedMoveSpeed = 1000f;
    float moveSpeed = 800f;
    bool isMoving = false;
    Animation animation;
    RectTransform handTransform;
    Canvas canvas;
    Item targetObject;
    bool applyComponent = false;
    Vector2 palettePoint;
    bool paletteAnimation = false;
    bool showLeftShadow = false;
    bool showRightShadow = false;

    void Awake()
    {
        main = this;
        canvas = GetComponentInParent<Canvas>();
    }

    void Start()
    {
        handTransform = HandController.main.GetTransform();
    }

    void Update()
    {
        if (isMoving && animation != null)
        {
            Vector2 movePoint;

            if (currentIndex >= animation.endBasedSpeedPosition)
            {
                moveSpeed = animation.moveSpeed;
            }
            else
            {
                moveSpeed = basedMoveSpeed;
            }

            if (paletteAnimation)
            {
                movePoint = palettePoint;

                Vector2 handLocalPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvas.GetComponent<RectTransform>(),
                    handTransform.position,
                    null,
                    out handLocalPos
                );

                if (Vector2.Distance(handLocalPos, palettePoint) < 0.1f)
                {
                    paletteAnimation = false;
                }
            }
            else
            {
                movePoint = animation.path[currentIndex].anchoredPosition;
            }

            handTransform.anchoredPosition = Vector2.MoveTowards(
                handTransform.anchoredPosition,
                movePoint,
                moveSpeed * Time.deltaTime
            );

            float distance = Vector2.Distance(
                handTransform.anchoredPosition,
                animation.path[currentIndex].anchoredPosition
            );

            if (distance < pointDistance && !paletteAnimation)
            {
                if (!applyComponent)
                {
                    BodyManager.main.ApplyingMakeup(targetObject);
                    applyComponent = true;
                }

                currentIndex++;

                if (animation != null && currentIndex >= animation.path.Count)
                {
                    isMoving = false;
                    applyComponent = false;
                    showLeftShadow = false;
                    showRightShadow = false;
                    currentIndex = 0;
                    handTransform.anchoredPosition = animation.path[animation.path.Count - 1].anchoredPosition;
                    HandController.main.ReturnHand();
                }
            }

            if (animation.type == ItemType.Shadows && currentIndex == animation.startPositionAnimation && !showLeftShadow)
            {
                BodyManager.main.ShowLeftShadow();            
                showLeftShadow = true;
            }

            if (animation.type == ItemType.Shadows && currentIndex == animation.startSecondPositionAnimation && !showRightShadow)
            {
                BodyManager.main.ShowRightShadow();
                showRightShadow = true;
            }
        }
        else if (isMoving && animation == null)
        {
            HandController.main.ReturnHand();
            showLeftShadow = false;
            showRightShadow = false;
        }
    }

    public void StartAnimation(Item item)
    {
        targetObject = item;
        isMoving = true;
        animation = animations.FirstOrDefault(anim => anim.type == item.type);

        if (item.type == ItemType.Blush || item.type == ItemType.Shadows)
        {
            BodyManager.main.HideShadows();    
            RectTransform targetRect = targetObject.GetTransform();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.GetComponent<RectTransform>(),
                targetRect.position,
                null,
                out palettePoint
            );
            palettePoint = new Vector2(palettePoint.x + palettePointOffsetX, palettePoint.y + palettePointOffsetY);
            paletteAnimation = true;
        }

        if (animation != null && animation.path.Count > 0)
        {
            currentIndex = 0;
        }
    }
}
