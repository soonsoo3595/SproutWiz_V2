using DG.Tweening;
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

    public void SetTileColor(Color newColor)
    {
        tileSprite.color = newColor;
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


    public void PlayAnim(GrowPoint growPoint)
    {
        if(growPoint == GrowPoint.Seed)
        {
            PlayAnimSeed();
        }
        else if(growPoint == GrowPoint.Harvest)
        {
            PlayAnimHarvest();
        }
    }

    public void PlayAnimSeed()
    {
        
    }

    public void PlayAnimHarvest()
    {

    }
}
