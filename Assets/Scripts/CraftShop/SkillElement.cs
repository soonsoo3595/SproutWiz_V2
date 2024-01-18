using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillElement : MonoBehaviour
{
    [Header("Object")]
    public Image image;
    public TextMeshProUGUI title;
    public List<GameObject> levelIcon;
    public TextMeshProUGUI upgradeBtnText;
    public Button upgradeBtn;

    [Header("Data")]
    public SkillType skillType;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }

    public void SetSkill(Skill skill)
    {
        image.sprite = skill.sprite;
        title.text = skill.title;
        skillType = skill.type;

        int maxLevel = DataManager.skillLibrary.GetMaxLevel(skillType);
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        if(curLevel == maxLevel)
        {
            upgradeBtnText.text = "최대 레벨 도달";
            upgradeBtn.interactable = false;
        }

        for(int i = 0; i < maxLevel; i++)
        {
            SetIcon(i, i < curLevel);
        }
    }

    private void SetIcon(int idx, bool isOn)
    {
        levelIcon[idx].SetActive(true);

        Image image = levelIcon[idx].GetComponent<Image>();

        if(isOn)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.grey;
        }
    }

    private void Click()
    {
        EventManager.setSkillInfoPopup?.Invoke(skillType);
    }
}
