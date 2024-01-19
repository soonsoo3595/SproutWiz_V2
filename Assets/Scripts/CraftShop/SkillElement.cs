using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private int maxLevel;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(InfoClick);
        upgradeBtn.onClick.AddListener(UpgradeClick);
    }

    public void SetSkill(Skill skill)
    {
        image.sprite = skill.sprite;
        title.text = skill.title;
        skillType = skill.type;

        maxLevel = DataManager.skillLibrary.GetMaxLevel(skillType);
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        SetButton(curLevel);

        for(int i = 0; i < maxLevel; i++)
        {
            SetIcon(i, i < curLevel);
        }
    }

    public void AfterUpgrade()
    {
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        SetButton(curLevel);

        StartCoroutine(GreyToGreen(curLevel - 1));
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

    private void SetButton(int curLevel)
    {
        if(curLevel == maxLevel)
        {
            upgradeBtnText.text = "최대 레벨 도달";
            upgradeBtn.interactable = false;
        }
        else
        {
            upgradeBtnText.text = $"{DataManager.skillLibrary.GetCost(skillType, curLevel)}G";
            upgradeBtn.interactable = true;
        }
    }

    IEnumerator GreyToGreen(int idx)
    {
        Image image = levelIcon[idx].GetComponent<Image>();

        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            time += Time.deltaTime;

            float lerpFactor = Mathf.Clamp01(time / duration);

            Color lerpedColor = Color.Lerp(Color.gray, Color.green, lerpFactor);

            image.color = lerpedColor;

            yield return null;
        }
    }

    private void InfoClick()
    {
        EventManager.setSkillInfo?.Invoke(skillType);
    }

    private void UpgradeClick()
    {
        EventManager.setSkillUpgrade?.Invoke(skillType, this);
    }
}
