using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] private Transform tileVisual;
    //[SerializeField] private Transform cropVisual;

    SpriteRenderer tileSprite;

    private void Awake()
    {
        tileSprite = tileVisual.GetComponent<SpriteRenderer>();
    }

    public void SetTileColor(Color newColor)
    {
        tileSprite.color = newColor;
    }
}
