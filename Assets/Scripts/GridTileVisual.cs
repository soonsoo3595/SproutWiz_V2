using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] private Transform tileVisual;
    [SerializeField] private Transform cropVisual;
    [SerializeField] private Transform outLine;
    [SerializeField] private Animator animator;
    [SerializeField] private GridTileEffecter effectObject;

    [SerializeField] private Transform blink;

    SpriteRenderer tileSprite;
    SpriteRenderer cropSprite;
    SpriteRenderer outLineSprite;
    SpriteRenderer blinkSprite;

    Animator CropAnim;

    private void Awake()
    {
        tileSprite = tileVisual.GetComponent<SpriteRenderer>();
        cropSprite = cropVisual.GetComponent<SpriteRenderer>();
        outLineSprite = outLine.GetComponent<SpriteRenderer>();
        blinkSprite = blink.GetComponent<SpriteRenderer>();

        CropAnim = cropSprite.GetComponent<Animator>();
    }

    public void SetTileColor(Color newColor)
    {
        tileSprite.color = newColor;
    }

    public void SetTileSptire(Sprite sprite)
    {
        tileSprite.sprite = sprite;
    }

    public void ResetTileColor()
    {
        tileSprite.color = Color.white;
    }

    public void SetCropSptire(Sprite sprite)
    {
        cropSprite.sprite = sprite;
    }


    public void SetOutLineColor(Color newColor)
    {
        outLineSprite.color = newColor;
    }

    public void SetOutLineAlpha(float alpha)
    {
        Color color = outLineSprite.color;
        
        color.a = alpha;
        
        outLineSprite.color = color;
    }


    public void SetActiveBlink(bool isActive)
    {
        blink.gameObject.SetActive(isActive);

        Color color = blinkSprite.color;
        color.a = 1f;
        blinkSprite.color = color;

        if (isActive)
        {
            blinkSprite.DOFade(0.2f, 1).SetLoops(-1, LoopType.Yoyo);
        }
    }

    
    // trigger로 함수이름변경 필요.
    public void PlayAnimHarvest()
    {
        CropAnim.SetTrigger("Harvest");
    }

    public void AnimDepressIs(bool param)
    {
        CropAnim.SetBool("OnNegative", param);
    }

    public void ResetAnimation()
    {
        
    }


    // Effect
    public void PlayHarvestEffect(ElementType element)
    {
        effectObject.PlayHarvestEffect(element);
    }

    public void PlayDeadEffect(ElementType element)
    {
        effectObject.PlayDeadEffect(element);
    }
}
