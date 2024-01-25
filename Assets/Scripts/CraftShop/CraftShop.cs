using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CraftShop : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI goldTxt;

    [Header("Object")]
    public List<GameObject> skillCategory;
    public GameObject skillPrefab;

    [Header("Popup")]
    public SkillInfo skillInfo;
    public GameObject skillInfoPopup;
    public GameObject skillInfoBack;


    void Start()
    {
        UpdateGold();
        StartCoroutine(Init());
    }

    void OnEnable()
    {
        EventManager.setSkillInfo += skillInfo.SetPopup;
        EventManager.updateUI += UpdateGold;
    }

    void OnDisable()
    {
        EventManager.setSkillInfo -= skillInfo.SetPopup;
        EventManager.updateUI -= UpdateGold;
    }

    public void UpdateGold()
    {
        goldTxt.text = DataManager.playerData.gold.ToString("N0");
    }

    private IEnumerator Init()
    {
        List<Skill> skills = DataManager.skillLibrary.skills;

        if (skills == null)
        {
            yield break;
        }

        foreach(var skill in skills)
        {
            GameObject skillObject = Instantiate(skillPrefab);
            SkillElement skillElement = skillObject.GetComponent<SkillElement>();
            skillObject.transform.SetParent(skillCategory[(int)skill.category].transform, false); 

            skillObject.GetComponent<PopupBtn>().Register(skillInfoPopup, skillInfoBack);
            skillElement.SetSkill(skill);
        }

        yield return null;
    }
}
