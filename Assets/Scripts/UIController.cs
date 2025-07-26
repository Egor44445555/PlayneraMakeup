using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    public static UIController main;

    void Awake()
    {
        main = this;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HandController.main.SetTargetPosition(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandController.main.SetTargetPosition(eventData.position);
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        HandController.main.EndTouch(eventData);
    }
}
