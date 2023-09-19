using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Element
{
    ElementType elementType;

    public Element()
    {
        elementType = ElementType.None;
    }

    public Element(ElementType elementType)
    {
        this.elementType = elementType;
    }

    public override bool Equals(object obj)
    {
        return obj is Element element &&
               elementType == element.elementType;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(elementType);
    }

    public override string ToString()
    {
        return $"Element : {elementType}";
    }

    public static bool operator ==(Element tile, Element unit)
    {
        return tile.elementType == unit.elementType;
    }

    public static bool operator !=(Element tile, Element unit)
    {
        return tile.elementType != unit.elementType;
    }

    public static bool operator >(Element tile, Element unit)
    {
        if(tile.elementType == ElementType.Fire)
        {
            if (unit.elementType == ElementType.Grass)
                return true;
            else
                return false;
        }
        else if(tile.elementType == ElementType.Water)
        {
            if (unit.elementType == ElementType.Fire)
                return true;
            else
                return false;
        }
        else if (tile.elementType == ElementType.Grass)
        {
            if (unit.elementType == ElementType.Water)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <(Element tile, Element unit)
    {
        if (tile.elementType == ElementType.Fire)
        {
            if (unit.elementType == ElementType.Water)
                return true;
            else
                return false;
        }
        else if (tile.elementType == ElementType.Water)
        {
            if (unit.elementType == ElementType.Grass)
                return true;
            else
                return false;
        }
        else if (tile.elementType == ElementType.Grass)
        {
            if (unit.elementType == ElementType.Fire)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public ElementRelation GetElementRelation(Element element)
    {
        if (this == element)
        {
            Debug.Log("加己 包拌 悼殿");
            return ElementRelation.Equal;
        }
        else if (this < element)
        {
            Debug.Log("加己 包拌 阂府");
            return ElementRelation.Disadvantage;
        }
        else if (this > element)
        {
            Debug.Log("加己 包拌 快困");
            return ElementRelation.Advantage;
        }
        else
            return ElementRelation.Equal;
    }


    public ElementType GetElementType() => elementType;

    public void SetElementType(ElementType elementType) { this.elementType = elementType; }

    public void InitElement() { elementType = ElementType.None; }
    
}
