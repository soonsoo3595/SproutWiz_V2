using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfo : MonoBehaviour
{
    [Header("Object")]
    public Image logo;
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Button upgradeBtn;
    public SkillUpgrade upgradePopup;
    public Sprite[] buttonSprites;

    private SkillType skillType;
    private int maxLevel;

    public void SetPopup(SkillElement skillElement)
    {
        skillType = skillElement.skillType;
        maxLevel = DataManager.skillLibrary.GetMaxLevel(skillType);

        Skill skill = DataManager.skillLibrary.Get(skillType);

        logo.sprite = skill.sprite;
        title.text = skill.title;

        #region 스킬 설명 부분(길어서 region 처리)
        description.text = skill.preDesc;

        string effect = "";

        if (skillType == SkillType.MultiHarvest || skillType == SkillType.DrawLine)
        {
            effect += "\n";
            for (int i = 0; i < 3; i++)
            {
                if (skillType == SkillType.MultiHarvest)
                {
                    effect += $"{i + 2}개 : ";
                }
                else if (skillType == SkillType.DrawLine)
                {
                    effect += $"{i + 3}개 : ";
                }

                for (int j = 1; j <= DataManager.skillLibrary.GetMaxLevel(skillType); j++)
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
                if (effectValue < 1f) effectValue *= 100f;
                effect += effectValue;
                effect += "</color>";

                effect += "/";
            }

            effect = effect.Substring(0, effect.Length - 1);
        }

        description.text += effect;
        description.text += skill.sufDesc;
        #endregion

        SetButton();

        upgradePopup.Assign(skillElement);
    }

    private void SetButton()
    {
        TextMeshProUGUI upgradeBtnText = upgradeBtn.GetComponentInChildren<TextMeshProUGUI>();
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        if (curLevel == maxLevel)
        {
            upgradeBtn.GetComponent<Image>().sprite = buttonSprites[1];
            upgradeBtnText.text = "";
            upgradeBtn.interactable = false;
        }
        else
        {
            upgradeBtn.GetComponent<Image>().sprite = buttonSprites[0];
            upgradeBtnText.text = $"{DataManager.skillLibrary.GetCost(skillType, curLevel)}G";
            upgradeBtn.interactable = true;
        }
    }
}
