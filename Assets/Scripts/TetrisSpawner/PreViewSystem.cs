//using System;
using System.Collections.Generic;
using UnityEngine;

public class PreViewSystem
{
    private int slotNum;
    private int tetrisVariantCount;

    Transform[] TetrisPrefabs;

    private int[] countSpanwNum;

    public PreViewSystem(int slotNum)
    {
        this.slotNum = slotNum;
        countSpanwNum = new int[4];

        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        string path = "Prefabs/Tetris/";

        TetrisPrefabs = Resources.LoadAll<Transform>(path);
        tetrisVariantCount = TetrisPrefabs.Length;

        if (tetrisVariantCount <= 0)
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
        ElementType randomElement = (ElementType)Random.Range((int)ElementType.Fire, (int)ElementType.Grass + 1);

        // ������ Ȯ�ο�.
        countSpanwNum[(int)randomElement]++;

        return new Element(randomElement);
    }

    public Element GetSecondElement(Element baseElement)
    {
        List<int> list = new List<int>() { 1, 2, 3 };
        list.Remove((int)baseElement.GetElementType());

        Element secondElement = new Element((ElementType)list[Random.Range(0, 2)]);

        return secondElement;
    }


    public void PrintStatistics()
    {
        Debug.Log($"�Ӽ� ���� ����\n" +
            $"Fire : {countSpanwNum[1]}, Water : {countSpanwNum[2]}, Grass : {countSpanwNum[3]}");
    }

}

public struct TetrisSpawnSetting
{
    public float singleElementRatio;
    public float doubleElementRatio;

    public TetrisSpawnSetting(float singleRadio, float doubleRatio)
    {
        singleElementRatio = singleRadio;
        doubleElementRatio = doubleRatio;
    }
}