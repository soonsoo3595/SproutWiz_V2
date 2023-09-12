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

        TetrisPrefabs = Resources.LoadAll<Transform>("Prefabs/Tetris/");
        tetrisVariantCount = TetrisPrefabs.Length;

        if(tetrisVariantCount <= 0)
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
        int temp = UnityEngine.Random.Range((int)Element.Fire, (int)Element.Grass + 1);

        if(Enum.IsDefined(typeof(Element), temp))
        {
            return (Element)temp;
        }
        else
        {
            Debug.Log("속성 부여 에러");

            return Element.None;
        }
    }
}
