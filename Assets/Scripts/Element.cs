using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

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
        return $"{elementType}";
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
        return tile.elementType switch
        {
            ElementType.Fire when unit.elementType == ElementType.Grass => true,
            ElementType.Fire => false,
            ElementType.Water when unit.elementType == ElementType.Fire => true,
            ElementType.Water => false,
            ElementType.Grass when unit.elementType == ElementType.Water => true,
            ElementType.Grass => false,
            _ => false
        };
    }

    public static bool operator <(Element tile, Element unit)
    {
        return tile.elementType switch
        {
            ElementType.Fire when unit.elementType == ElementType.Water => true,
            ElementType.Fire => false,
            ElementType.Water when unit.elementType == ElementType.Grass => true,
            ElementType.Water => false,
            ElementType.Grass when unit.elementType == ElementType.Fire => true,
            ElementType.Grass => false,
            _ => false
        };
    }

    public ElementRelation GetElementRelation(Element element)
    {
        if (this == element)
        {
            return ElementRelation.Equal;
        }
        else if (this < element)
        {
            return ElementRelation.Disadvantage;
        }
        else if (this > element)
        {
            return ElementRelation.Advantage;
        }
        else
            return ElementRelation.Equal;
    }


    public ElementType GetElementType() => elementType;

    public void SetElementType(ElementType elementType) { this.elementType = elementType; }

    public void Init() { elementType = ElementType.None; }

    public bool IsNone() => elementType == ElementType.None;
}
