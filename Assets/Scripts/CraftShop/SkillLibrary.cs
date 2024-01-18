using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SkillLibrary", menuName = "ScriptableObjects/SkillLibrary", order = 1)]
public class SkillLibrary : ScriptableObject
{
    public List<Skill> skills;

    public Skill Get(SkillType skillType)
    {
        int idx = (int)skillType;
        
        if(idx < 0 || idx >= skills.Count)
        {
            return null;
        }

        return skills[idx];
    }

    public float GetEffect(SkillType skillType, int level)
    {
        Skill skill = Get(skillType);

        if(skill == null || level <= 0 || level > skill.effectList[0].effect.Count)
        {
            return 0f;
        }

        return skill.effectList[0].effect[level - 1];
    }

    public float GetEffect(SkillType skillType, int index, int level)
    {
        Skill skill = Get(skillType);

        if (skill == null || level <= 0 || level > skill.effectList[0].effect.Count)
        {
            return 0f;
        }

        return skill.effectList[index].effect[level - 1];
    }

    public int GetCurrentLevel(SkillType skillType)
    {
        int level = DataManager.playerData.skillLevels[(int)skillType];

        return level;
    }
    
    public int GetMaxLevel(SkillType skillType)
    {
        int maxLevel = Get(skillType).effectList[0].effect.Count;

        return maxLevel;
    }
}
