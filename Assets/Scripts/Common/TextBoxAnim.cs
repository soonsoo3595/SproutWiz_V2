using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxAnim : MonoBehaviour
{
    [SerializeField] Vector2 SizeMultiplie = new Vector2(1f, 1f);
    [SerializeField] float PopupSpeed = 0.3f;

    Image box;

    Vector3 orignScale;
    Vector3 transfomDelta;

    private void Start()
    {
        box = GetComponent<Image>();

        orignScale = transform.localScale;
        transfomDelta = new Vector3(0.05f, 0.05f, 0) * SizeMultiplie;

        transform.DOScale(transform.localScale + transfomDelta, PopupSpeed);
    }

    private void OnEnable()
    {
        transform.DOScale(transform.localScale + transfomDelta, PopupSpeed);
    }

    private void OnDisable()
    {
        transform.localScale = orignScale;
    }
}
