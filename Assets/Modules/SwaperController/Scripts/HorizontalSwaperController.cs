using UnityEngine;
using UnityEngine.EventSystems;

public class HorizontalSwaperController : ASwaperBaseController, IDragHandler, IEndDragHandler {


    public void OnDrag(PointerEventData eventData) {

        Horizontal = eventData.delta.x / Screen.width;

    }

    public void OnEndDrag(PointerEventData eventData) {
        Horizontal = 0;
    }
}
