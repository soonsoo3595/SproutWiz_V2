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
    DrawLine,
    HuntBird,
    MiniGameGoldReward,
    ScrollMastery,
    ManaEfficiency,
    Overclock,
    SunshineMagicMastery,
    END
}

public enum EMinigameType
{
    DrawLine,
    Griffon,
    Meteor
}

public enum SceneType
{
    Title,
    MainGame,
    CraftShop,
    Town,
    Loading,
    Tutorial
}

public enum RecordType
{
    Harvest,
    MultiHarvest,
    ClearGoal,
    DrawLine,
    Griffon,
    Meteor
}