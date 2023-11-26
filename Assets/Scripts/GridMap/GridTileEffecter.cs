using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileEffecter : MonoBehaviour
{
    [SerializeField] GameObject FireHarvest;
    [SerializeField] GameObject WaterHarvest;
    [SerializeField] GameObject GrassHarvest;

    public void PlayEffect(ElementType element)
    {
        switch (element)
        {
            case ElementType.Fire:
                FireHarvest.SetActive(true);
                WaterHarvest.SetActive(false);
                GrassHarvest.SetActive(false);
                break;
            case ElementType.Water:
                FireHarvest.SetActive(false);
                WaterHarvest.SetActive(true);
                GrassHarvest.SetActive(false);
                break;
            case ElementType.Grass:
                FireHarvest.SetActive(false);
                WaterHarvest.SetActive(false);
                GrassHarvest.SetActive(true);
                break;
        }
    }

}
