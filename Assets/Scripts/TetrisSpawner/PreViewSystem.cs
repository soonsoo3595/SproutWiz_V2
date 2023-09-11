using UnityEngine;

public class PreViewSystem
{
    private int slotNum;
    private int tetrisVariantCount;

    Transform[] TetrisPrefabs;

    public PreViewSystem(int slotNum)
    {
        this.slotNum = slotNum;

        TetrisPrefabs = Resources.LoadAll<Transform>("Prefabs/Tetris/");
        tetrisVariantCount = TetrisPrefabs.Length;

        if(tetrisVariantCount <= 0)
        {
            Debug.Log("경로에 생성 가능한 테트리스 없음");
        }
    }

    public Transform GetNewTetris()
    {
       return TetrisPrefabs[GetRandomNum()];
    }


    private int GetRandomNum()
    {
        return Random.Range(0, tetrisVariantCount);
    }


}
