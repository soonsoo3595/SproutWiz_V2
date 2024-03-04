using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBlinker : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    Color OriginColor;

    private void Awake()
    {
        OriginColor = sprite.color;
    }

    private void OnEnable()
    {
        sprite.color = OriginColor;
    }
}
