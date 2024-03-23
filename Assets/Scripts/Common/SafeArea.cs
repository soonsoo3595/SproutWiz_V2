using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    void Start()
    {
        Vector2 minAnchor;
        Vector2 maxAnchor;

        var myRect = GetComponent<RectTransform>();

        minAnchor = Screen.safeArea.min;
        maxAnchor = Screen.safeArea.max;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;

        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        myRect.anchorMin = minAnchor;
        myRect.anchorMax = maxAnchor;
    }

}
