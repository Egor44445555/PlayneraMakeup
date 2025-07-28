using UnityEngine;
using System.Linq;

public class AnimationsHand : MonoBehaviour
{
    public static AnimationsHand main;
    [SerializeField] Animation[] animations;

    [Header("Movement Settings")]
    [SerializeField] float pointDistance = 0.5f;

    int currentIndex = 0;
    float basedMoveSpeed = 3000f;
    float moveSpeed = 800f;
    bool isMoving = false;
    Animation animation;
    RectTransform handTransform;
    Canvas canvas;
    Item targetObject;
    bool applyComponent = false;

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
            if (currentIndex >= animation.startPositionAnimation)
            {
                moveSpeed = animation.moveSpeed;
            }
            else
            {
                moveSpeed = basedMoveSpeed;
            }

            handTransform.anchoredPosition = Vector2.MoveTowards(
                handTransform.anchoredPosition,
                animation.path[currentIndex].anchoredPosition,
                moveSpeed * Time.deltaTime
            );

            float distance = Vector2.Distance(
                handTransform.anchoredPosition,
                animation.path[currentIndex].anchoredPosition
            );

            if (distance < pointDistance)
            {
                if (!applyComponent)
                {
                    print(currentIndex);
                    BodyManager.main.ApplyingMakeup(targetObject);
                    applyComponent = true;
                }

                currentIndex++;

                if (animation != null && currentIndex >= animation.path.Count)
                {
                    isMoving = false;
                    applyComponent = false;
                    currentIndex = 0;
                    handTransform.anchoredPosition = animation.path[animation.path.Count - 1].anchoredPosition;
                    HandController.main.ReturnHand();
                }
            }
        }
        else if (isMoving && animation == null)
        {
            HandController.main.ReturnHand();
        }
    }

    public void StartAnimation(Item item)
    {
        targetObject = item;
        isMoving = true;
        animation = animations.FirstOrDefault(anim => anim.type == item.type);

        if (animation != null && animation.path.Count > 0)
        {
            currentIndex = 0;
            isMoving = true;
        }
    }
}
