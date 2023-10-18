using System.Collections.Generic;
using UnityEngine;

public class TetrisViewPanel : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> ViewSlots;

    [SerializeField]
    private float TetrisSize = 50;

    private List<Transform> tetrisList;

    private PreViewSystem preViewSystem;


    private void Awake()
    {
        preViewSystem = new PreViewSystem(ViewSlots.Count);
        tetrisList = new List<Transform>();
    }

    private void Start()
    {
        SetTetrisToAllSlot();

        LevelData.applyTetris += UpdateTetrisSlot;
        GridManager.clearGrid += ResetAllSlot;
    }

    private void OnDestroy()
    {
        preViewSystem.PrintStatistics();
    }

    private void SetTetrisToAllSlot()
    {
        foreach (RectTransform slot in ViewSlots)
        {
            AddNewTetris();
        }

        RelocateTetris();
    }

    private void ResetAllSlot()
    {
        foreach (Transform tetris in tetrisList)
        {
            Destroy(tetris.gameObject);
        }

        tetrisList.Clear();
        SetTetrisToAllSlot();
    }

    private void UpdateTetrisSlot(TetrisObject tetrisObject)
    {
        ReplaceTetris(tetrisObject);
        RelocateTetris();
    }



    private void ReplaceTetris(TetrisObject tetrisObject)
    {
        tetrisList.Remove(tetrisObject.transform);
        AddNewTetris();
    }

    private void RelocateTetris()
    {
        int count = 0;

        foreach (Transform tetris in tetrisList)
        {
            tetris.SetParent(ViewSlots[count]);
            SetDefaultSize(tetris);

            count++;
        }
    }

    private void AddNewTetris()
    {
        tetrisList.Add(SpawnTetris());
    }

    private void SetDefaultSize(Transform tetris)
    {
        tetris.localScale = new Vector3(TetrisSize, TetrisSize);
        tetris.localPosition = Vector3.zero;
    }


    private Transform SpawnTetris()
    {
        Transform newTetris = Instantiate(preViewSystem.GetRandomTetris());
        StateSetting(newTetris.GetComponent<TetrisObject>());
        Rotate(newTetris);

        return newTetris;
    }

    private void Rotate(Transform newTetris)
    {
        float rotation = Random.value;

        if (rotation <= 0.25f)
        {

        }
        else if (rotation <= 0.5f)
        {
            newTetris.Rotate(new Vector3(0f, 0f, 90f));
        }
        else if (rotation <= 0.75f)
        {
            newTetris.Rotate(new Vector3(0f, 0f, 180f));
        }
        else
        {
            newTetris.Rotate(new Vector3(0f, 0f, 270f));
        }
    }

    private void StateSetting(TetrisObject Tetris)
    {
        Element baseElement = preViewSystem.GetRandomElement();
        GameSetting setting = GridManager.Instance.GetSetting();

        Tetris.SetAllUnitState(baseElement);


        if (!setting.mixBlockEnable) return;
        if(Random.value > setting.singleElementRatio)
        {
            int unitNum = Random.Range(0, Tetris.GetUnitCount());
            Element secondElement = preViewSystem.GetSecondElement(baseElement);

            Tetris.SetUnitState(unitNum, secondElement);
        }
    }

    

}
