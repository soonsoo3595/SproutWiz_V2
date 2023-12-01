using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSFX : MonoBehaviour
{
    private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySFX);
    }

    public void PlaySFX()
    {
        Debug.Log("Click");
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("click");
    }
}
