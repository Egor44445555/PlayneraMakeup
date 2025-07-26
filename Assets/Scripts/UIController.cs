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
        HandController.main.SetTargetPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandController.main.SetTargetPosition(eventData);
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        HandController.main.EndTouch(eventData);
    }
}
