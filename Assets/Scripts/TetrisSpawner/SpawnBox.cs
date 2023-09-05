using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBox : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponentInChildren<TetrisObject>().AttachMouse(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponentInChildren<TetrisObject>().AttachMouse(false);
    }
}
