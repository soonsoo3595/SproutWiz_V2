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

    [Header("Data")]
    public SkillType skillType;

    private int maxLevel;
    private Color transparent = new Color(1f, 1f, 1f, 0f);
    private Color opaque = new Color(1f, 1f, 1f, 1f);

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(InfoClick);
    }

    public void SetSkill(Skill skill)
    {
        image.sprite = skill.sprite;
        title.text = skill.title;
        skillType = skill.type;

        maxLevel = DataManager.skillLibrary.GetMaxLevel(skillType);
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        for(int i = 0; i < maxLevel; i++)
        {
            SetIcon(i, i < curLevel);
        }
    }

    public void AfterUpgrade()
    {
        int curLevel = DataManager.skillLibrary.GetCurrentLevel(skillType);

        StartCoroutine(UpgradeAnimation(curLevel - 1));
    }

    private void SetIcon(int idx, bool isOn)
    {
        levelIcon[idx].SetActive(true);

        GameObject gameObject = levelIcon[idx].transform.GetChild(0).gameObject;

        if(isOn)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    

    IEnumerator UpgradeAnimation(int idx)
    {
        GameObject gameObject = levelIcon[idx].transform.GetChild(0).gameObject;
        Image image = gameObject.GetComponent<Image>();

        float time = 0f;
        float duration = 1f;

        gameObject.SetActive(true);
        image.color = transparent;

        while (time < duration)
        {
            time += Time.deltaTime;

            float lerpFactor = Mathf.Clamp01(time / duration);

            Color lerpedColor = Color.Lerp(transparent, opaque, lerpFactor);

            image.color = lerpedColor;

            yield return null;
        }
    }

    private void InfoClick()
    {
        EventManager.setSkillInfo?.Invoke(this);
    }
}
