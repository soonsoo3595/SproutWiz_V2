using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class StartPoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    DrawLineGame Master;


    public void SetMaster(DrawLineGame Master)
    {
        this.Master = Master;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Master.SetIsDrag(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Master.SetIsDrag(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
