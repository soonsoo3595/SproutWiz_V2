using UnityEngine;

[CreateAssetMenu(fileName = "GamSetting", menuName = "MyGame/Game Settings", order = 1)]
public class GameSetting : ScriptableObject
{
    // ���� ����
    public int GridMapWidth = 5;
    public int GridMapHeight = 5;

    public int timeLimit = 10;

    // �̸����� ����
    public float DistanceFromHand = 500f;
    public float DistanceFromTetris_x = 0f;
    public float DistanceFromTetris_y = 0f;

    // ��Ʈ���� �Ӽ� ȥ�� ����
    public bool mixBlockEnable = false;

    public float singleElementRatio = 1f;
    public float doubleElementRatio = 0f;


    // �Ӽ����迡 ���� ����ġ ����
    public GrowPointSet equal;
    public GrowPointSet disadvantage;
    public GrowPointSet irrelevant;

    public TetrisSpawnSet tetrisSpawnSet;


}

[System.Serializable]
public class GrowPointSet
{
    public int growPoint;
    public float percentage;

    public GrowPointSet(int growPoint, float percentage)
    {
        this.growPoint = growPoint;
        this.percentage = percentage;
    }
}

[System.Serializable]
public class TetrisSpawnSet
{
    public int[] typeRatio = new int[7];

    public float singleElementRatio = 1f;
    public float doubleElementRatio = 0f;

    public TetrisSpawnSet()
    {
    }

}
