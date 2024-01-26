using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBox : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    bool isGameRunning;

    private void Start()
    {
        isGameRunning = true;

        EventManager.mainGameOver += DisableRunning;
        EventManager.resetMainGame += EnableRunnig;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(isGameRunning)
            GetComponentInChildren<TetrisObject>().AttachMouse(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isGameRunning)
            GetComponentInChildren<TetrisObject>().AttachMouse(false);
    }

    private void EnableRunnig()
    {
        isGameRunning = true;

        gameObject.SetActive(true);
    }

    private void DisableRunning()
    {
        isGameRunning = false;

        gameObject.SetActive(false);
    }
}
