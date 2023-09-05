using System.Collections.Generic;
using UnityEngine;

public class TetrisViewPanel : MonoBehaviour
{
    [SerializeField]
    private List<RectTransform> ViewSlots;

    [SerializeField]
    private List<Transform> TetrisPrefabs;

    private List<Transform> tetrisList;

    private PreViewSystem preViewSystem;


    private void Awake()
    {
        preViewSystem = new PreViewSystem(ViewSlots.Count, TetrisPrefabs.Count);
        tetrisList = new List<Transform>();
    }

    private void Start()
    {
        foreach (RectTransform slot in ViewSlots)
        {
            Transform tetris = Instantiate(TetrisPrefabs[preViewSystem.GetRandomNum()], slot);
            tetris.localScale = new Vector3(50f, 50f, 50f);
            tetris.localPosition = Vector3.zero;

            tetrisList.Add(tetris);
        }

        EventManager.Instance.applyTetris += ReplaceSlot;
    }

    private void ReplaceSlot(TetrisObject tetrisObject)
    {
        tetrisList.Remove(tetrisObject.transform);
        tetrisList.Add(Instantiate(TetrisPrefabs[preViewSystem.GetRandomNum()]));

        int count = 0;

        foreach(Transform tetris in tetrisList)
        {
            tetris.SetParent(ViewSlots[count]);
            tetris.localScale = new Vector3(50f, 50f, 50f);
            tetris.localPosition = Vector3.zero;

            count++;
        }
    }
}
