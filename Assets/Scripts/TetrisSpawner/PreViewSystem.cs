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
            Debug.Log("��ο� ���� ������ ��Ʈ���� ����");
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
            Debug.Log("�Ӽ� �ο� ����");

            return Element.None;
        }
    }
}
