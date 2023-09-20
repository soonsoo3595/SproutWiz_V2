using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileVisual : MonoBehaviour
{
    [SerializeField] private Transform tileVisual;
    //[SerializeField] private Transform cropVisual;

    public void SetTileColor(Color newColor)
    {
        tileVisual.GetComponent<SpriteRenderer>().color = newColor;
    }
}
