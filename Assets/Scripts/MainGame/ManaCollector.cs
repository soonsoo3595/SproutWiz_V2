using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaCollector : MonoBehaviour
{
    public MainGame mainGame;
    public Image disable;
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
            disable.fillAmount = (float)mana / (float)maxMana;
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
        Debug.Log("마나 충전 : " + Mana + " / " + maxMana);

        if(Mana >= maxMana && !mainGame.isMagicOn)
        {
            OnMagic();
        }
    }

    private void Assign()
    {
        #region 햇빛마법 시간
        {
            int level = DataManager.playerData.level_SunshineMagicTime;
            magicTime = DataManager.GameData.sunshineMagicTime + DataManager.GameData.upgrade_sunshineMagicEffect[level];
        }
        #endregion

        #region 햇빛마법 효과
        {
            int level = DataManager.playerData.level_ReduceManaCollectorMaxMana;
            maxMana = DataManager.GameData.manaCollectorMaxMana + DataManager.GameData.reduce_ManaCollectorMaxMana[level];
        }
        #endregion

        #region 애니메이션 부분
        animationDuration = magicAnim.GetCurrentAnimatorStateInfo(0).length;
        animationSpeed = animationDuration / magicTime;
        magicAnim.speed = 0f;
        #endregion

        #region 마나 응집기 최대 스택
        {
            int level = DataManager.playerData.level_ReduceManaCollectorMaxMana;
            maxMana = DataManager.GameData.manaCollectorMaxMana - DataManager.GameData.reduce_ManaCollectorMaxMana[level];
        }
        #endregion

        #region 수확 시 획득하는 스택
        {
            normalHarvestMana = DataManager.GameData.manaCollectorHarvestMana;
            multiHarvestMana = DataManager.GameData.manaCollectorMultiHarvestMana;
        }
        #endregion

        #region 이벤트 등록
        EventManager.harvestCount += ChargeMana;
        EventManager.resetMainGame += ResetGame;
        #endregion
    }

    private void OnMagic()
    {
        mainGame.isMagicOn = true;

        mainGame.gameRecord.feverCount++;
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
