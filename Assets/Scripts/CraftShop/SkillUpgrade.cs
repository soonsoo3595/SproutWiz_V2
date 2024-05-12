using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpgrade : MonoBehaviour
{
    public GameObject notifyTxt;
    public TextMeshProUGUI content;
    public Button btn;

    private int upgradeCost;
    private SkillType skillType;
    private SkillElement skillElement;

    void Start()
    {
        btn.onClick.AddListener(Upgrade);
    }

    void OnEnable()
    {
        SetPopup();
    }

    public void Assign(SkillElement skillElement)
    {
        skillType = skillElement.skillType;
        this.skillElement = skillElement;
    }

    public void SetPopup()
    {
        Skill skill = DataManager.skillLibrary.Get(skillType);
        upgradeCost = skill.costs[DataManager.skillLibrary.GetCurrentLevel(skillType)];

        string str = "";
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        str += $"{skill.title} ";
        str += $"<color=yellow>{curLevel + 1}´Ü°è</color>";

        content.text = str;
    }    

    public void Upgrade()
    {
        if(DataManager.playerData.gold >= upgradeCost)
        {
            DataManager.playerData.gold -= upgradeCost;
            DataManager.playerData.skillLevels[(int)skillType]++;

            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("upgrade");
            skillElement.AfterUpgrade();
            EventManager.updateUI();

            GameManager.Instance.Save();
        }
        else
        {
            return;
        }

        BackMgr.instance.Pop();
    }

}
