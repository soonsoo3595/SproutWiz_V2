using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [Header("Object")]
    [SerializeField] GameObject logo;
    [SerializeField] TextMeshProUGUI guideTxt;
    [SerializeField] Button startBtn;
    [SerializeField] Transform start;
    [SerializeField] Transform end;

    [SerializeField] GameObject signOutBtn; // 로그아웃 버튼  

    void OnEnable()
    {
        EventManager.EnterGame += ShowAnimation;
    }

    private void OnDisable()
    {
        EventManager.EnterGame -= ShowAnimation;
    }

    private void ShowAnimation()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        logo.transform.position = start.position;
        logo.transform.DOMove(end.position, 1.0f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1.5f);

        guideTxt.gameObject.SetActive(true);
        guideTxt.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        signOutBtn.SetActive(true);
        startBtn.interactable = true;

        yield return null;
    }
}
