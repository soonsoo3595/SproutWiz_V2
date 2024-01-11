using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReRollSystem : MonoBehaviour
{
    public MainGame mainGame;
    public Button rerollBtn;
    public Image disable;
    public TetrisViewPanel tetrisViewPanel;

    private int rerollCount = 0;
    private float chargeTime;
    private float coolTime = 0f;
    public float CoolTime
    {
        get => coolTime;
        set
        {
            coolTime = Mathf.Clamp(value, 0, chargeTime);
            disable.fillAmount = coolTime / chargeTime;
        }
    }

    void Start()
    {
        rerollBtn.onClick.AddListener(ReRoll);

        Assign();
    }

    private void Assign()
    {
        {
            int level = DataManager.playerData.level_ReduceCastingCancel;
            chargeTime = DataManager.GameData.castingCancelChargeTime - DataManager.GameData.reduce_CastingCancel[level];
        }

        #region 이벤트 등록
        EventManager.resetMainGame += RetryGame;
        #endregion
    }

    private void ReRoll()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("reroll");
        rerollCount++;

        tetrisViewPanel.ReRoll();
        StartCoolTime();
    }

    private void StartCoolTime()
    {
        rerollBtn.interactable = false;
        CoolTime = 0f;
        StartCoroutine(CoolDown());
    }

    public void RetryGame()
    {
        CoolTime = chargeTime;
        rerollCount = 0;
        rerollBtn.interactable = true;
    }

    IEnumerator CoolDown()
    {
        while (CoolTime < chargeTime)
        {
            if (mainGame.isGameOver) yield break;

            if (mainGame.isPaused)
            {
                yield return null;
            }
            else
            {
                CoolTime += Time.deltaTime;

                yield return new WaitForFixedUpdate();
            }
        }

        rerollBtn.interactable = true;
    }
}
