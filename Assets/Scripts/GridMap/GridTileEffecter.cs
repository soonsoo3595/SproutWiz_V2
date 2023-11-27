using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GridTileEffecter : MonoBehaviour
{
    [SerializeField] GameObject FireHarvest;
    [SerializeField] GameObject WaterHarvest;
    [SerializeField] GameObject GrassHarvest;

    [SerializeField] GameObject DeadEffect;

    public void PlayHarvestEffect(ElementType element)
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

    public void PlayDeadEffect(ElementType element)
    {
        ParticleSystem particleSystem = DeadEffect.GetComponent<ParticleSystem>();
        MainModule particleModule = particleSystem.main;

        particleSystem.gameObject.SetActive(true);

        switch (element)
        {
            case ElementType.Fire:
                particleModule.startColor = Color.red;
                break;
            case ElementType.Water:
                particleModule.startColor = Color.blue;
                break;
            case ElementType.Grass:
                particleModule.startColor = Color.green;
                break;
            default:
                particleModule.startColor = Color.black;
                break;
        }
       
    }

}
