using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
    public Image logo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void SetPopup(SkillType skillType)
    {
        Skill skill = DataManager.skillLibrary.Get(skillType);

        logo.sprite = skill.sprite;
        title.text = skill.title;

        #region 스킬 설명 부분(길어서 region 처리)
        description.text = skill.preDesc;

        string effect = "";

        if(skillType == SkillType.MultiHarvest || skillType == SkillType.DrawStroke)
        {
            effect += "\n";
            for(int i = 0; i < 3; i++)
            {
                if(skillType == SkillType.MultiHarvest)
                {
                    effect += $"{i + 2}개 : ";
                }
                else if(skillType == SkillType.DrawStroke)
                {
                    effect += $"{i + 3}개 : ";
                }

                for(int j = 1; j <= DataManager.skillLibrary.GetMaxLevel(skillType); j++)
                {
                    if (j == DataManager.skillLibrary.GetCurrentLevel(skillType))
                    {
                        effect += "<color=yellow>";
                    }
                    else
                    {
                        effect += "<color=grey>";
                    }

                    effect += DataManager.skillLibrary.GetEffect(skillType, i, j);
                    effect += "</color>";

                    effect += "/";
                }
                effect = effect.Substring(0, effect.Length - 1);
                effect += "\n";
            }
        }
        else
        {
            for (int i = 1; i <= DataManager.skillLibrary.GetMaxLevel(skillType); i++)
            {
                if (i == DataManager.skillLibrary.GetCurrentLevel(skillType))
                {
                    effect += "<color=yellow>";
                }
                else
                {
                    effect += "<color=grey>";
                }

                float effectValue = DataManager.skillLibrary.GetEffect(skillType, i);
                if(effectValue < 1f)    effectValue *= 100f;
                effect += effectValue;
                effect += "</color>";

                effect += "/";
            }

            effect = effect.Substring(0, effect.Length - 1);
        }

        description.text += effect;
        description.text += skill.sufDesc;
        #endregion
    }    

}
