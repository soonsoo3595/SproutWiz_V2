using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillEffect
{
    public List<float> effect;
}

[CreateAssetMenu(fileName = "Skill", menuName = "ScriptableObjects/Skill", order = 1)]
public class Skill : ScriptableObject
{
    public Sprite sprite;
    public SkillCategoryType category;
    public SkillType type;
    public string title;

    // 아래꺼 변수 이름 건들이면 데이터 날라가니까 조심!!!!
    public string preDesc;
    public string sufDesc;
    public List<SkillEffect> effectList;

    public List<int> costs;
    
}
