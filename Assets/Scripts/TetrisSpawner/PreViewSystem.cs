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
        Element newElement = new Element();

        int randomElement = UnityEngine.Random.Range((int)ElementType.Fire, (int)ElementType.Grass + 1);

        if (Enum.IsDefined(typeof(ElementType), randomElement))
        {
            newElement.SetElementType((ElementType)randomElement);
        }
        else
        {
            Debug.Log("속성 부여 에러");
        }

        return newElement;
    }
}
