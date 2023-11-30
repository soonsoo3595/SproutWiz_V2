using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnSFX : MonoBehaviour
{
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(PlaySFX);
    }

    public void PlaySFX()
    {
        GameManager.Instance.soundEffect.PlayOneShotSoundEffect("click");
    }
}
