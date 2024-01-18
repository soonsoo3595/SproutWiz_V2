using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftShop : MonoBehaviour
{
    [Header("Object")]
    public List<GameObject> skillCategory;
    public GameObject skillPrefab;

    [Header("Popup")]
    public SkillInfo skillInfo;
    public GameObject upgradePopup;
    public GameObject upgradeBack;
    public GameObject skillInfoPopup;
    public GameObject skillInfoBack;


    void Start()
    {
        StartCoroutine(Init());
    }

    void OnEnable()
    {
        EventManager.setSkillInfoPopup += skillInfo.SetInfo;
    }

    void OnDisable()
    {
        EventManager.setSkillInfoPopup -= skillInfo.SetInfo;
    }

    public void ShowPopup()
    {

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
            GameObject skillElement = Instantiate(skillPrefab);
            skillElement.transform.SetParent(skillCategory[(int)skill.category].transform, false); 

            skillElement.GetComponent<PopupBtn>().Register(skillInfoPopup, skillInfoBack);
            skillElement.GetComponent<SkillElement>().SetSkill(skill);
        }

        yield return null;
    }
}
