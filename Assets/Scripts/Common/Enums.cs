using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums : MonoBehaviour
{
    
}

public enum GrowPoint { Seed, Growth, Harvest }

public enum ElementType { None, Fire, Water, Grass }

public enum ElementRelation { Disadvantage, Equal, Irrelevant }

public enum GoalType {None, A, B, C, D}

public enum ButtonType { Popup, Back }

public enum SkillCategoryType
{
    Farm,
    MiniGame,
    Utility
}

public enum SkillType
{
    Harvest,
    MultiHarvest,
    GoalReward,
    DrawStroke,
    CarvingStone,
    HuntBird,
    MiniGameGoldReward,
    ScrollMastery,
    ManaEfficiency,
    Overclock,
    SunshineMagicMastery,
    EquipMastery
}

public enum EMinigameType
{
    DrawLine,
    Griffon,
    Meteor
}