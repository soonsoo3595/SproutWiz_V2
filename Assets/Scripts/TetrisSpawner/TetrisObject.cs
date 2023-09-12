using System.Collections.Generic;
using UnityEngine;

public class TetrisObject : MonoBehaviour
{
    [SerializeField]
    private bool isAttackedMouse;
    private TetrisUnit[] units;

    private void Awake()
    {
        isAttackedMouse = false;
        units = GetComponentsInChildren<TetrisUnit>();
    }

    private void Update()
    {
        FollowingMousePoint(isAttackedMouse);
    }

    public void AttachMouse(bool toggle)
    {
        isAttackedMouse = toggle;

        if (isAttackedMouse)
        {
            transform.localScale = new Vector3(250f, 250f, 250f);
        }
        else
        {
            ReleaseDrag();
        }
    }

    private void ReleaseDrag()
    {
        if (CheckAllUnitOnGrid())
        {
            Debug.Log("테트리스 배치 성공");

            LevelData.applyTetris(this);
            Destroy(gameObject);
        }
        else
        {
            transform.localScale = new Vector3(50f, 50f, 50f);
            transform.localPosition = Vector3.zero;
        }
    }

    private void FollowingMousePoint(bool isAttackedMouse)
    {
        if (!isAttackedMouse) return;

        Vector2 newPos = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

        transform.position = newPos;
    }

    private bool CheckAllUnitOnGrid()
    {
        foreach (TetrisUnit unit in units)
        {
            if (!unit.GetOnGrid()) return false;
        }

        return true;
    }

    public void SetAllUnitState(int growPoint, Element element)
    {
        foreach (TetrisUnit unit in units)
        {
            unit.SetUnitState(growPoint, element);
        }
    }

    public List<TetrisUnit> GetUnitList()
    {
        List<TetrisUnit> result = new List<TetrisUnit>(units);

        return result;
    }

}
