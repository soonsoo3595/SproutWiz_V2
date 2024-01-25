using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GriffonObject : MonoBehaviour, IPointerClickHandler
{
    public Vector2 minPosition;
    public Vector2 maxPosition;
    public float speed = 2.0f;

    private Vector3 targetPosition;

    void Start()
    {
        SetRandomTargetPosition();

        maxPosition = GridManager.Instance.GetWorldPosition(new GridPosition(4, 4));
        minPosition = GridManager.Instance.GetWorldPosition(new GridPosition(0, 0));
    }

    void Update()
    {
        MoveToTargetPosition();

        // 목표 위치에 도달하면 새로운 위치 설정
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        targetPosition = new Vector3(randomX, randomY, transform.position.z);
    }

    void MoveToTargetPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("그리핀 클릭");

        // TODO: 성공처리
        EventManager.miniGameSuccess(EMinigameType.Griffon);

        Destroy(gameObject);
    }

}
