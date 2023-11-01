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
        outLineSprite.color = newColor;
    }

    public void SetCropSptire(Sprite sprite)
    {
        cropSprite.sprite = sprite;
    }

    // �Ӽ� ���Ҹ� �ӽ� ǥ�ÿ�
    public void SetAlpha(float alpha)
    {
        Color color = outLineSprite.color;
        
        color.a = alpha;
        
        outLineSprite.color = color;
    }
}
