using UnityEngine;

public class PreViewSystem
{
    private int slotNum;
    private int tetrisVariant;

    public PreViewSystem(int slotNum, int tetrisVariant)
    {
        this.slotNum = slotNum;
        this.tetrisVariant = tetrisVariant;
    }

    public int GetRandomNum()
    {
        return Random.Range(0, tetrisVariant);
    }


}
