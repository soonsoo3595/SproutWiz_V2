using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboEffect : MonoBehaviour
{
    public GameObject doubleEffect;
    public GameObject tripleEffect;
    public GameObject quadraEffect;

    private void Start()
    {
    }

    public void Play(int count)
    {
        switch(count)
        {
            case 2:
                doubleEffect.SetActive(true);
                tripleEffect.SetActive(false);
                quadraEffect.SetActive(false);
                break;
            case 3:
                doubleEffect.SetActive(false);
                tripleEffect.SetActive(true);
                quadraEffect.SetActive(false);
                break;
            case 4:
                doubleEffect.SetActive(false);
                tripleEffect.SetActive(false);
                quadraEffect.SetActive(true);
                break;
        }
    }
}
