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
    [SerializeField] int MaxSpawnableCount = 4;

    [SerializeField] GameObject particlePrefab;

    List<Transform> griffonList = new List<Transform>();
    //Transform griffonObject;

    int spawnedGriffonCount = 0;

    float RecentExcuteTimeInIntervar;

    int IMiniGame.ActivationScore => ActivationScore;

    float IMiniGame.IntervalTime => IntervalTime;

    float IMiniGame.MinimumTerm => MinimumTerm;

    bool isActivate;
    bool IMiniGame.IsActivate => isActivate;

    bool isRunnig;
    bool IMiniGame.IsRunnig => isRunnig;

    private void Start()
    {
        ResetParams();

        EventManager.mainGameOver += Exit;
        EventManager.resetMainGame += ResetParams;
    }

    public void ResetParams()
    {
        isActivate = false;
        isRunnig = false;

        spawnedGriffonCount = 0;

        RecentExcuteTimeInIntervar = Random.Range(0, IntervalTime);
        Debug.Log($"그리핀 최초 실행시간 : {RecentExcuteTimeInIntervar}");
    }

    public void Activate(float activateTime)
    {
        if (isActivate)
            return;

        Debug.Log("미니게임:그리핀 조건 달성");

        isActivate = true;

        UpdateExcuteTime();
    }

    public void Excute()
    {
        if(spawnedGriffonCount >= MaxSpawnableCount)
        {
            Debug.Log("그리핀 최대치 활동 중");

            UpdateExcuteTime();
            MiniGameController.Instance.ExitMiniGame(this);

            return;
        }

        Debug.Log("그리핀 실행!");
        spawnedGriffonCount++;

        GridPosition startGridPosition = SelectStartPoint();
        Vector3 startPointWorldPos = GridManager.Instance.GetWorldPosition(startGridPosition);
        startPointWorldPos.z = 10;

        Transform griffonObject = Instantiate(GriffonPrefab, startPointWorldPos, Quaternion.identity, CanvasWorldSpace);
        griffonObject.GetComponent<GriffonObject>().SetMaster(this);

        griffonList.Add(griffonObject);

        isRunnig = true;

        if(!GameManager.Instance.isTutorial)
        {
            UpdateExcuteTime();
            MiniGameController.Instance.ExitMiniGame(this);
        }
    }

    public void Exit()
    {
        if (griffonList.Count != 0)
        {
            foreach(var griffon in griffonList)
            {
                Destroy(griffon.gameObject);
            }

            griffonList.Clear();
        }
            
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

        Debug.Log($"그리핀 다음 실행 시간 : {RecentExcuteTimeInIntervar}");
    }

    public float GetNextExcuteTime()
    {
        return RecentExcuteTimeInIntervar;
    }

    public bool GetAcivate()
    {
        return isActivate;
    }


    // 마나맥과 중복함수.
    private GridPosition SelectStartPoint()
    {
        int gridSizeX = GridManager.Instance.GetWidth();
        int gridSizeY = GridManager.Instance.GetHeight();

        int x = Random.Range(0, gridSizeX);
        int y = Random.Range(0, gridSizeY);

        return new GridPosition(x, y);
    }

    public void PlayEffect(Vector3 pos)
    {
        //if (griffonObject == null)
        //    return;

        pos.z = 94;
        spawnedGriffonCount--;
        Instantiate(particlePrefab, pos, Quaternion.identity); 
    }


    int toturialTargetCount = 0;

    public void ExcuteTutorial()
    {
        Debug.Log("튜토리얼 그리폰 실행!");

        Excute();
        Excute();
    }

    public void TutorialSuccess()
    {
        toturialTargetCount++;

        if(toturialTargetCount == 2)
        {
            MiniGameController.Instance.GriffonSuccess();
        }
    }
}
