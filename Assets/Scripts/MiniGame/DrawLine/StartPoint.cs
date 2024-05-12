using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    DrawLineGame Master;

    [SerializeField] Sprite activateImage;
    [SerializeField] Sprite deactivateImage;

    Image sourceImage;

    private void Start()
    {
        sourceImage = GetComponent<Image>();
    }

    public void SetMaster(DrawLineGame Master)
    {
        this.Master = Master;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Master.SetIsDrag(true);

        SetActiveImage(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    public void EndDrag()
    {
        Master.SetIsDrag(false);

        SetActiveImage(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void PlayEffect(bool isSuccess)
    {
        sourceImage.enabled = false;

        if (isSuccess)
        {

        }
        else
        {

        }
    }

    public void SetActiveImage(bool Drag)
    {
        if(Drag)
        {
            sourceImage.sprite = activateImage;
        }
        else
        {
            sourceImage.sprite = deactivateImage;
        }
    }
}
