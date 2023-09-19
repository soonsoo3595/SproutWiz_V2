using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TetrisUnit : TileUnit
{
    int growPoint;
    Element element;

    private void Start()
    {
        SetBlockColor();
    }

    public void SetUnitState(int growPoint, Element element)
    {
        this.growPoint = growPoint;
        this.element = element;
    }

    public int GetGrowPoint()
    {
        return growPoint;
    }

    public Element GetElement()
    {
        return element;
    }


    private void SetBlockColor()
    {
        Color blockColor = new Color();

        switch (element.GetElementType())
        {
            case ElementType.None:
                blockColor = Color.white;
                break;
            case ElementType.Fire:
                blockColor = Color.red;
                break;
            case ElementType.Water:
                blockColor = Color.blue;
                break;
            case ElementType.Grass:
                blockColor = Color.green;
                break;
        }

        blockColor.a *= 0.7f;

        GetComponentInChildren<SpriteRenderer>().color = blockColor;
    }
}
