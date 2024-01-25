using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CraftShop : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI goldTxt;
    public GameObject topUI;
    public GameObject upgradeUI;
    public Image back;

    [Header("Object")]
    public List<GameObject> skillCategory;
    public GameObject skillPrefab;

    [Header("Popup")]
    public SkillInfo skillInfo;
    public GameObject skillInfoPopup;
    public GameObject skillInfoBack;


    void Start()
    {
        StartCoroutine(EnterCraftShop());
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

    private IEnumerator EnterCraftShop()
    {
        topUI.SetActive(false);
        upgradeUI.SetActive(false);
        back.DOFade(1f, 1.5f);
        yield return new WaitForSeconds(1.5f);

        topUI.SetActive(true);
        UpdateGold();
        
        upgradeUI.SetActive(true);
        StartCoroutine(Init());
    }

}
