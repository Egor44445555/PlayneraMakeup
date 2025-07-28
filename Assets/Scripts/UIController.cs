using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public static UIController main;

    void Awake()
    {
        main = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!HandController.main.IsAnimating())
        {
            HandController.main.SetTargetPosition(eventData.position);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!HandController.main.IsAnimating())
        {
            HandController.main.SetTargetPosition(eventData.position);
        }
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        HandController.main.EndTouch(eventData.position);
    }
}
