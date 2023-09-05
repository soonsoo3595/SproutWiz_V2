using Unity.VisualScripting;
using UnityEngine;

public class TetrisObject : MonoBehaviour
{
    [SerializeField]
    private bool isAttackedMouse;
    private TileUnit[] units;

    private void Awake()
    {
        isAttackedMouse = false;
        units = GetComponentsInChildren<TileUnit>();
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
            transform.localScale = new Vector3(300f, 300f, 300f);
        }
        else
        {
            ReleseDrag();
        }
    }

    private void ReleseDrag()
    {
        if (CheckAllUnitOnGrid())
        {
            Debug.Log("테트리스 배치 성공");
            EventManager.Instance.applyTetris(this);
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
        foreach (TileUnit unit in units)
        {
            if (!unit.GetOnGrid()) return false;
        }

        return true;
    }
}
