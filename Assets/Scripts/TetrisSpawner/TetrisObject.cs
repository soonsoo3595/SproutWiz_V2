using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TetrisObject : MonoBehaviour
{
    [SerializeField]
    private bool isAttackedMouse;
    private TetrisUnit[] units;
    private Transform[] visuals;

    readonly Vector3 InSlotScale = new Vector3(50f, 50f, 50f);
    readonly Vector3 InFieldScale = new Vector3(257f, 257f, 257f);

    private void Awake()
    {
        isAttackedMouse = false;
        units = GetComponentsInChildren<TetrisUnit>();
        visuals = new Transform[units.Length];
    }

    private void Start()
    {
        for (int i = 0; i < units.Length; i++)
        {
            visuals[i] = units[i].GetComponentInChildren<SpriteRenderer>().transform;
        }
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
            EnableSeparation();
        }
        else
        {
            ReleaseDrag();
        }
    }

    private void ReleaseDrag()
    {
        if (CheckAllUnitOnGrid() && CheckAllUnitDeployable())
        {
            LevelData.applyTetris(this);

            Destroy(gameObject);

            //MiniGameController.Instance.ExecuteMiniGame();
        }
        else
        {
            DisableSeparation();
        }
    }

    private void EnableSeparation()
    {
        transform.localScale = InFieldScale;

        foreach (Transform visual in visuals)
        {
            visual.position += new Vector3(
                GridManager.Instance.GetSetting().DistanceFromTetris_x,
                GridManager.Instance.GetSetting().DistanceFromTetris_y
                );
        }
    }

    private void DisableSeparation()
    {
        transform.localScale = InSlotScale;
        transform.localPosition = Vector3.zero;

        foreach (Transform visual in visuals)
        {
            visual.localPosition = new Vector3(0, 0);
        }
    }

    private void FollowingMousePoint(bool isAttackedMouse)
    {
        if (!isAttackedMouse) return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));

        transform.position = newPos;
        transform.localPosition += new Vector3(0, GridManager.Instance.GetSetting().DistanceFromHand);
    }

    private bool CheckAllUnitOnGrid()
    {
        foreach (TetrisUnit unit in units)
        {
            if (!unit.GetOnGrid()) return false;
        }

        return true;
    }

    private bool CheckAllUnitDeployable()
    {
        foreach (TetrisUnit unit in units)
        {
            if (!unit.GetDeployable()) return false;
        }

        return true;
    }


    public void SetUnitState(int unitNumber, Element element)
    {
        if (unitNumber < 0 && unitNumber > GetUnitCount())
        {
            Debug.Log("잘못된 유닛 번호");
            return;
        }

        units[unitNumber].SetUnitState(element);
    }

    public void SetAllUnitState(Element element)
    {
        foreach (TetrisUnit unit in units)
        {
            unit.SetUnitState(element);
        }
    }

    public int GetUnitCount()
    {
        return units.Length;
    }

    public List<TetrisUnit> GetUnitList()
    {
        List<TetrisUnit> result = new List<TetrisUnit>(units);

        return result;
    }

}