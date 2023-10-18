//using System;
using System.Collections.Generic;
using UnityEngine;

public class PreViewSystem
{
    private int slotNum;
    private int tetrisVariantCount;

    Transform[] TetrisPrefabs;

    private int[] countSpanwNum;
    private int[] countSpanwType;

    public PreViewSystem(int slotNum)
    {
        this.slotNum = slotNum;
        countSpanwNum = new int[4];
        countSpanwType = new int[8];

        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        string path = "Prefabs/Tetris/";

        TetrisPrefabs = Resources.LoadAll<Transform>(path);
        tetrisVariantCount = TetrisPrefabs.Length;

        if (tetrisVariantCount <= 0)
        {
            Debug.Log("경로에 생성 가능한 테트리스 없음");
        }
    }


    public Transform GetRandomTetris()
    {
        int randomNum = Random.Range(0, 100);

        int tempRatioSum = 0;
        for(int i = 0; i < 7; i++)
        {
            tempRatioSum += GridManager.Instance.GetSetting().tetrisSpawnSet.typeRatio[i];

            if (randomNum <= tempRatioSum)
            {
                countSpanwType[i]++;
                return TetrisPrefabs[i];
            }
        }

        countSpanwType[7]++;
        return TetrisPrefabs[Random.Range(0, tetrisVariantCount)];
    }

    public Element GetRandomElement()
    {
        ElementType randomElement = (ElementType)Random.Range((int)ElementType.Fire, (int)ElementType.Grass + 1);

        // 랜덤값 확인용.
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
        Debug.Log($"속성 생성 개수\n" +
            $"Fire : {countSpanwNum[1]}, Water : {countSpanwNum[2]}, Grass : {countSpanwNum[3]}");

        for (int i = 0; i < 7; i++)
        {
            Debug.Log($"타입 {(char)'A' + 1} 생성 개수 : {countSpanwType[i]}");
        }

        Debug.LogWarning($"타입 생성 에러 : {countSpanwType[7]}개 발생");
    }

}
