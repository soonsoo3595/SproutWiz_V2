using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public GameObject logo;
    public GameObject guide;
    public TextMeshProUGUI guideTxt;
    public Button startBtn;

    public Transform start;
    public Transform end;

    private void Start()
    {
        startBtn.interactable = false;
        guide.SetActive(false);

        StartCoroutine(BeforeStart());
    }

    IEnumerator BeforeStart()
    {
        logo.transform.position = start.position;
        logo.transform.DOMove(end.position, 1.0f).SetEase(Ease.OutBounce);

        yield return new WaitForSeconds(1.0f);

        guide.SetActive(true);
        guideTxt.DOFade(1.0f, 1.0f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);

        startBtn.interactable = true;

        yield return null;
    }
}
