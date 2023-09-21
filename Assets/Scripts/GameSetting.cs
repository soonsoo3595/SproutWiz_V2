using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GamSetting", menuName = "MyGame/Game Settings", order = 1)]
public class GameSetting : ScriptableObject
{
    public int GridMapWidth = 5;
    public int GridMapHeight = 5;

    public float PercentageOfMixTetris = 0;
    public bool EnableMix_Three_Type = false;

    public float probabilityA = 1f;
    public float probabilityB = 1f;
    public float probabilityC = 1f;

    public bool EnableMix_Two_Type = false;

    public float probabilityD = 1f;
    public float probabilityE = 1f;
}


