using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDrawLine : MonoBehaviour
{
    [SerializeField] List<RectTransform> points;

    private void Start()
    {
        if (points.Count < 4)
            return;

        points[0].position = SetPos(1, 4);
        points[1].position = SetPos(1, 3);
        points[2].position = SetPos(2, 3);
        points[3].position = SetPos(3, 3);

        for(int i = 0;  i < points.Count; i++)
        {
            SetZeroZ(points[i]);
        }
    }

    private Vector2 SetPos(int x, int y)
    {
        Vector2 pos = GridManager.Instance.GetWorldPosition(new GridPosition(x, y));

        return pos;
    }

    private void SetZeroZ(RectTransform rect)
    {
        rect.localPosition = new Vector3(rect.localPosition.x, rect.localPosition.y, 0f);
    }
}
