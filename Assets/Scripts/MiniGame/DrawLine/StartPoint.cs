using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    DrawLineGame Master;

    [SerializeField] Sprite activateImage;
    [SerializeField] Sprite deactivateImage;

    [SerializeField] GameObject SuccessParticleObject;

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

        sourceImage.sprite = activateImage;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Master.SetIsDrag(false);

        sourceImage.sprite = deactivateImage;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void PlayEffect(bool isSuccess)
    {
        if(isSuccess)
        {
            SuccessParticleObject.SetActive(true);
        }
        else
        {

        }
    }
}
