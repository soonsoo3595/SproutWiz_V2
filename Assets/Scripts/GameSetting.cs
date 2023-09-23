using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GamSetting", menuName = "MyGame/Game Settings", order = 1)]
public class GameSetting : ScriptableObject
{
    public int GridMapWidth = 5;
    public int GridMapHeight = 5;


    public bool mixBlockEnable = false;

    public float fireRatio = 1f;
    public float waterRatio = 0f;
    public float grassRatio = 0f;
}


