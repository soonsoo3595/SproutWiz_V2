using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingCancel : MonoBehaviour
{
    public MainGame mainGame;
    public Button rerollBtn;
    public List<Image> batteries;
    public TetrisViewPanel tetrisViewPanel;

    private float chargeTime;

    private int chargeStack = 0;
    private int ChargeStack
    {
        get => chargeStack;
        set
        {
            chargeStack = Math.Clamp(value, 0, 3);

            for(int i = 0; i < 3; i++)
            {
                batteries[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < chargeStack; i++)
            {
                batteries[i].gameObject.SetActive(true);
                batteries[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    private float battery = 0f;
    public float Battery
    {
        get => battery;
        set
        {
            battery = Mathf.Clamp(value, 0, chargeTime);

            if (ChargeStack != 3)
            {
                batteries[ChargeStack].gameObject.SetActive(true);
                batteries[ChargeStack].color = new Color(1f, 1f, 1f, 0.5f);
                batteries[ChargeStack].fillAmount = battery / chargeTime;
            }
        }
    }

    void Start()
    {
        rerollBtn.onClick.AddListener(Cancel);

        Assign();
    }

    public void ChargeStart()
    {
        StartCoroutine(Charge());
    }

    public void RetryGame()
    {
        ChargeStack = 1;
        Battery = 0f;
    }

    private void Assign()
    {
        {
            int level = DataManager.skillLibrary.GetCurrentLevel(SkillType.ScrollMastery);
            chargeTime = DataManager.GameData.CastingCancelChargeTime;

            if (level != 0)
            {
                chargeTime -= DataManager.skillLibrary.GetEffect(SkillType.ScrollMastery, level);
            }

            Debug.Log("스크롤 마스터리 레벨 : " + level + ", 충전 시간 : " + chargeTime);
        }

        ChargeStack = 1;

        #region 이벤트 등록
        EventManager.resetMainGame += RetryGame;
        #endregion
    }

    private void Cancel()
    {
        if(ChargeStack == 0 || mainGame.isGameOver)
        {
            return;
        }

        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("reroll");

        ChargeStack -= 1;

        StopAllCoroutines();

        tetrisViewPanel.ReRoll();
        ChargeStart();
    }

    IEnumerator Charge()
    {
        while (ChargeStack < 3)
        {
            while (Battery < chargeTime)
            {
                if (mainGame.isGameOver) yield break;

                if (mainGame.isPaused)
                {
                    yield return null;
                }
                else
                {
                    Battery += Time.deltaTime;

                    yield return new WaitForFixedUpdate();
                }
            }

            ChargeStack += 1;
            Battery = 0f;
        }
    }
}
