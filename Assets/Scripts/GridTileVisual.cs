using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] private Transform tileVisual;
    [SerializeField] private Transform cropVisual;
    [SerializeField] private Transform outLine;

    SpriteRenderer tileSprite;
    SpriteRenderer cropSprite;
    SpriteRenderer outLineSprite;

    private void Awake()
    {
        tileSprite = tileVisual.GetComponent<SpriteRenderer>();
        cropSprite = cropVisual.GetComponent<SpriteRenderer>();
        outLineSprite = outLine.GetComponent<SpriteRenderer>();
    }

    public void SetCropSptire(Sprite sprite)
    {
        cropSprite.sprite = sprite;
    }


    public void SetOutLineColor(Color newColor)
    {
        outLineSprite.color = newColor;
    }

    // 속성 유불리 임시 표시용
    public void SetOutLineAlpha(float alpha)
    {
        Color color = outLineSprite.color;
        
        color.a = alpha;
        
        outLineSprite.color = color;
    }
}
