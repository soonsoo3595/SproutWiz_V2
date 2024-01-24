using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaCollector : MonoBehaviour
{
    public MainGame mainGame;
    public Image energy;
    public Image energy_light;
    public Animator magicAnim;
    public AudioSource audioSource;

    private int normalHarvestMana;
    private List<int> multiHarvestMana;

    private int mana = 0;
    private int maxMana;

    private float magicTime;
    private float animationDuration = 0f;
    private float animationSpeed = 0f;

    public int Mana
    {
        get => mana;
        set
        {
            mana = Mathf.Clamp(value, 0, maxMana);
            energy.fillAmount = (float)mana / (float)maxMana;

            energy_light.gameObject.SetActive(true);
            energy_light.color = new Color(1f, 1f, 1f, energy.fillAmount);

            // if (energy.fillAmount >= 0.5f)
            // {
            //     energy_light.gameObject.SetActive(true);
            //     energy_light.color = new Color(1f, 1f, 1f, energy.fillAmount);
            // }
            // else
            // {
            //     energy_light.gameObject.SetActive(false);
            // }
        }
    }

    void Start()
    {
        Assign();

        ResetGame();
    }

    public void ResetGame()
    {
        mainGame.isMagicOn = false;
        Mana = 0;
    }

    public void ChargeMana(int count)
    {
        if (count == 0) return;

        Mana += (normalHarvestMana * count + multiHarvestMana[count]);
        Debug.Log("���� ���� : " + Mana + " / " + maxMana);

        if(Mana >= maxMana && !mainGame.isMagicOn)
        {
            OnMagic();
        }
    }

    private void Assign()
    {
        #region �޺����� �ð�
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.Overclock);
            magicTime = DataManager.GameData.SunshineMagicTime;

            if (level != 0)
            {
                magicTime += DataManager.skillLibrary.GetEffect(SkillType.Overclock, level);
            }

            Debug.Log("����Ŭ�� ���� : " + level + ", ���� �ð� : " + magicTime);
        }
        #endregion

        #region �ִϸ��̼� �κ�
        animationDuration = magicAnim.GetCurrentAnimatorStateInfo(0).length;
        animationSpeed = animationDuration / magicTime;
        magicAnim.speed = 0f;
        #endregion

        #region ���� ������ �ִ� ����
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.ManaEfficiency);
            maxMana = DataManager.GameData.ManaCollectorMaxMana;

            if (level != 0)
            {
                maxMana -= (int)DataManager.skillLibrary.GetEffect(SkillType.ManaEfficiency, level);
            }

            Debug.Log("���� ȿ�� ���� ���� : " + level + ", �ִ� ���� : " + maxMana);
        }
        #endregion

        #region ��Ȯ �� ȹ���ϴ� ����
        {
            normalHarvestMana = DataManager.GameData.ManaCollectorHarvestMana;
            multiHarvestMana = DataManager.GameData.ManaCollectorMultiHarvestMana;
        }
        #endregion

        #region �̺�Ʈ ���
        EventManager.harvestCount += ChargeMana;
        EventManager.resetMainGame += ResetGame;
        #endregion
    }

    private void OnMagic()
    {
        mainGame.isMagicOn = true;

        // mainGame.gameRecord.feverCount++;
        StartCoroutine(ShowMagic());
    }

    private void OffMagic()
    {
        mainGame.isMagicOn = false;
        Mana = 0;
    }

    /*
    IEnumerator CoolDown()
    {
        while (FeverStack < maxStack)
        {
            if(mainGame.isGameOver) yield break;

            if (mainGame.isPaused)
            {
                yield return null;
            }
            else
            {
                FeverStack += 0.1f;

                yield return new WaitForSeconds(0.1f);
            }
        }

        magicBtn.interactable = true;
    }
    */

    IEnumerator ShowMagic()
    {
        magicAnim.speed = animationSpeed;
        magicAnim.Play("Fever", 0, 0f);
        audioSource.Play();

        while(magicAnim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            if(mainGame.isGameOver) break;

            if (mainGame.isPaused)
            {
                magicAnim.speed = 0f;
                yield return null;
                magicAnim.speed = animationSpeed;
            }
            else yield return new WaitForFixedUpdate();
        }

        magicAnim.speed = 0f;
        magicAnim.Play("Fever", 0, 0f);
        audioSource.Stop();

        OffMagic();
    }

}
