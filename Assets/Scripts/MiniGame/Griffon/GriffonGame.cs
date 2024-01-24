using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GriffonGame : MonoBehaviour, IMiniGame
{
    [SerializeField] Transform GriffonPrefab;
    [SerializeField] RectTransform CanvasWorldSpace;

    [SerializeField] int ActivationScore = 4000;
    [SerializeField] float IntervalTime = 45f;
    [SerializeField] float MinimumTerm = 20f;

    Transform GriffonObject;
    int SpawnedGriffonCount = 0;

    float RecentExcuteTimeInIntervar;

    int IMiniGame.ActivationScore => ActivationScore;

    float IMiniGame.IntervalTime => IntervalTime;

    float IMiniGame.MinimumTerm => MinimumTerm;

    bool isActivate;
    bool IMiniGame.IsActivate => isActivate;

    bool isRunnig;
    bool IMiniGame.IsRunnig => isRunnig;

    private void Awake()
    {
        isActivate = false;
        isRunnig = false;
    }

    private void Start()
    {
        RecentExcuteTimeInIntervar = Random.Range(0, IntervalTime);
        Debug.Log($"�׸��� ���� ����ð� : {RecentExcuteTimeInIntervar}");
    }

    public void Activate(float activateTime)
    {
        if (isActivate)
            return;

        Debug.Log("�̴ϰ���:�׸��� ���� �޼�");

        isActivate = true;

        UpdateExcuteTime();
    }

    public void Excute()
    {
        if(SpawnedGriffonCount >= 4)
        {
            Debug.Log("�׸��� �ִ�ġ Ȱ�� ��");
            return;
        }

        Debug.Log("�׸��� ����!");
        SpawnedGriffonCount++;

        GridPosition startGridPosition = SelectStartPoint();
        Vector3 startPointWorldPos = GridManager.Instance.GetWorldPosition(startGridPosition);
        startPointWorldPos.z = 10;

        GriffonObject = Instantiate(GriffonPrefab, startPointWorldPos, Quaternion.identity, CanvasWorldSpace);


        isRunnig = true;
    }

    public void Exit()
    {
        if (GriffonObject != null)
            Destroy(GriffonObject.gameObject);

        isRunnig = false;
    }

    private void UpdateExcuteTime()
    {
        float randomExcuteTime = Random.Range(0, IntervalTime);

        float term = (MinimumTerm - RecentExcuteTimeInIntervar) + randomExcuteTime;

        if (term < MinimumTerm)
        {
            RecentExcuteTimeInIntervar = randomExcuteTime + term;
        }
        else
        {
            RecentExcuteTimeInIntervar = randomExcuteTime;
        }

        Debug.Log($"�׸��� ���� ���� �ð� : {RecentExcuteTimeInIntervar}");
    }

    public float GetNextExcuteTime()
    {
        return RecentExcuteTimeInIntervar;
    }

    public bool GetAcivate()
    {
        return isActivate;
    }


    // �����ư� �ߺ��Լ�.
    private GridPosition SelectStartPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();

        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);

        return new GridPosition(x, y);
    }
}
