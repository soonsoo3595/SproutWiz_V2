using System;
using UnityEngine;

public class PreViewSystem
{
    private int slotNum;
    private int tetrisVariantCount;

    Transform[] TetrisPrefabs;

    public PreViewSystem(int slotNum)
    {
        this.slotNum = slotNum;

        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        TetrisPrefabs = Resources.LoadAll<Transform>("Prefabs/Tetris/");
        tetrisVariantCount = TetrisPrefabs.Length;

        if (tetrisVariantCount <= 0)
        {
            Debug.Log("경로에 생성 가능한 테트리스 없음");
        }
    }


    public Transform GetRandomTetris()
    {
        return TetrisPrefabs[UnityEngine.Random.Range(0, tetrisVariantCount)];
    }

    public Element GetRandomElement()
    {
        ElementType randomElement = (ElementType)UnityEngine.Random.Range((int)ElementType.Fire, (int)ElementType.Grass + 1);

        Element newElement = new Element(randomElement);

        return newElement;
    }

    public Element GetRandomElement(TetrisSpawnSetting setting)
    {
        float random = UnityEngine.Random.value;
        Element newElement;

        if (random <= setting.fireRatio)
        {
            newElement = new Element(ElementType.Fire);

            return newElement;
        }
        else if(random <= setting.fireRatio + setting.waterRatio)
        {
            newElement = new Element(ElementType.Water);

            return newElement;
        }
        else
        {
            newElement = new Element(ElementType.Grass);

            return newElement;
        }
    }
}

public struct TetrisSpawnSetting
{
    public int growPoint;
    public float fireRatio;
    public float waterRatio;
    public float grassRatio;

    public TetrisSpawnSetting(int growPoint, float fire, float water, float grass)
    {
        this.growPoint = growPoint;
        this.fireRatio = fire;
        this.waterRatio = water;
        this.grassRatio = grass;
    }
}