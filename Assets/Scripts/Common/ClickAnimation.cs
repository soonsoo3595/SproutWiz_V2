using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float changeScale = 0.9f;
    private Vector3 originScale;

    void Start()
    {
        originScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = originScale * changeScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale = originScale;
    }
}
