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
            Debug.Log("��ο� ���� ������ ��Ʈ���� ����");
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
