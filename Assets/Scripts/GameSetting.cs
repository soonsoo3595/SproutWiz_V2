using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GamSetting", menuName = "MyGame/Game Settings", order = 1)]
public class GameSetting : ScriptableObject
{
    // 게임 세팅
    public int GridMapWidth = 5;
    public int GridMapHeight = 5;

    public int timeLimit = 10;


    // 테트리스 속성 혼합 세팅
    public bool mixBlockEnable = false;

    public float singleElementRatio = 1f;
    public float doubleElementRatio = 0f;


    // 속성관계에 따른 성장치 세팅
    public GrowPointSet equal = new GrowPointSet(1, 100f);
    public GrowPointSet disadvantage = new GrowPointSet(1, 100f);
    public GrowPointSet irrelevant = new GrowPointSet(-1, 60f);


}

public class GrowPointSet
{
    public int growPoint;
    public float percentage;

    public GrowPointSet(int growPoint, float percentage)
    {
        this.growPoint = growPoint;
        this.percentage = percentage;
    }
}


