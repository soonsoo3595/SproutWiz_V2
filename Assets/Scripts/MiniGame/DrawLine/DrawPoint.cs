using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class DrawPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Sprite activateImage;
    [SerializeField] Sprite deactivateImage;

    [SerializeField] Material activeMaterial;
    [SerializeField] Material deactivateMaterial;

    UnityEngine.UI.Image sourceImage;

    LineRenderer lineRenderer;
    DrawLineGame Master;
    GridPosition gridPosition;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
        sourceImage = GetComponent<UnityEngine.UI.Image>();
    }

    public void SetMaster(DrawLineGame Master)
    {
        this.Master = Master;
    }

    public void DrawLine(GridPosition start, GridPosition end)
    {
        Vector3 startPos = new Vector3(start.x, start.y, 10);
        Vector3 endPos = new Vector3(end.x, end.y, 10);

        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Master.EnterDrawPoint(gridPosition, this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void SetActiveImage(bool active)
    {
        if(active)
        {
            sourceImage.sprite = activateImage;
            lineRenderer.material = activeMaterial;
        }
        else
        {
            sourceImage.sprite = deactivateImage;
            lineRenderer.material = deactivateMaterial;
        }
    }

    public void DisableImage()
    {
        lineRenderer.enabled = false;
        sourceImage.enabled = false;
    }
}
