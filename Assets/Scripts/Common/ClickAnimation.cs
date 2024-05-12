using UnityEngine;
using UnityEngine.EventSystems;

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
