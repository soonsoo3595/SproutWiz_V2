using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    public TextMeshProUGUI content;
    public Button btn;

    private int upgradeCost;
    private SkillType skillType;
    private SkillElement skillElement;

    void Start()
    {
        btn.onClick.AddListener(Upgrade);
    }

    public void SetPopup(SkillType skillType, SkillElement skillElement)
    {
        this.skillType = skillType;
        this.skillElement = skillElement;

        Skill skill = DataManager.skillLibrary.Get(skillType);
        upgradeCost = skill.costs[DataManager.skillLibrary.GetCurrentLevel(skillType)];

        string str = "";
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        str += $"{skill.title} ";
        str += $"<color=yellow>{curLevel + 1}단계</color>";

        content.text = str;
    }    

    public void Upgrade()
    {
        if(DataManager.playerData.gold >= upgradeCost)
        {
            DataManager.playerData.gold -= upgradeCost;
            DataManager.playerData.skillLevels[(int)skillType]++;

            skillElement.AfterUpgrade();
            EventManager.updateUI();
        }
        else
        {
            Debug.Log("골드 부족");
            return;
        }

        BackMgr.instance.Pop();
    }

}
