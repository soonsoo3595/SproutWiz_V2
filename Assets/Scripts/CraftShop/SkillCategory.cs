using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCategory : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Start()
    {
        toggle.onValueChanged.AddListener((value) =>
        {
            GameManager.Instance.soundEffect.PlayOneShotSoundEffect("click");

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(value);
            }
        });
    }

}
