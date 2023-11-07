using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteSetting", menuName = "MyGame/Sprite Settings", order = 2)]
public class SpriteSetting : ScriptableObject
{
    public Sprite Seed;

    public Sprite FireGrowth;
    public Sprite WaterGrowth;
    public Sprite GrassGrowth;

    public Sprite FireHarvest;
    public Sprite WaterHarvest;
    public Sprite GrassHarvest;


    public Transform TEST_FireGrowth;
    public Transform TEST_WaterGrowth;
    public Transform TEST_GrassGrowth;

    public Transform TEST_FireHarvest;
    public Transform TEST_WaterHarvest;
    public Transform TEST_GrassHarvest;
}
