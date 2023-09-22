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
        RelocateTetris();

        LevelData.applyTetris += UpdateTetrisSlot;
    }

    private void SetTetrisToAllSlot()
    {
        foreach (RectTransform slot in ViewSlots)
        {
            AddNewTetris();
        }
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

    private void AddNewTetris()
    {
        tetrisList.Add(SpawnTetris());
    }


    private Transform SpawnTetris()
    {
        Transform newTetris = Instantiate(preViewSystem.GetRandomTetris());

        StateSetting(newTetris.GetComponent<TetrisObject>());

        return newTetris;
    }

    private void StateSetting(TetrisObject Tetris)
    {
        //TetrisSpawnSetting setting = 

        Tetris.SetAllUnitState(1, preViewSystem.GetRandomElement());
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

    private void SetDefaultSize(Transform tetris)
    {
        tetris.localScale = new Vector3(TetrisSize, TetrisSize);
        tetris.localPosition = Vector3.zero;
    }
}
