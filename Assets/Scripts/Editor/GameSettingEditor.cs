using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
       //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        setting.GridMapWidth = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapHeight, 1, 10);

        setting.PercentageOfMixTetris = EditorGUILayout.Slider("ȥ�� ��Ʈ���� ���� Ȯ��", setting.PercentageOfMixTetris, 0, 100);
        setting.EnableMix_Three_Type = EditorGUILayout.Toggle("3�Ӽ� ȥ�� ����", setting.EnableMix_Three_Type);

        setting.probabilityA = EditorGUILayout.Slider("�� �Ӽ� ���� ����", setting.probabilityA, 0f, 100f);
        setting.probabilityB = EditorGUILayout.Slider("�� �Ӽ� ���� ����", setting.probabilityB, 0f, 100f);
        setting.probabilityC = EditorGUILayout.Slider("Ǯ �Ӽ� ���� ����", setting.probabilityC, 0f, 100f);


        setting.EnableMix_Two_Type = EditorGUILayout.Toggle("2�Ӽ� ȥ�� ����", setting.EnableMix_Two_Type);


        float totalProbability = 100f;
        float ratio1 = totalProbability / (setting.probabilityA + setting.probabilityB + setting.probabilityC);
        if (EditorGUI.EndChangeCheck())
        {
            float adjustedA = setting.probabilityA * ratio1;
            float adjustedB = setting.probabilityB * ratio1;
            float adjustedC = setting.probabilityC * ratio1;

            setting.probabilityA = adjustedA;
            setting.probabilityB = adjustedB;
            setting.probabilityC = adjustedC;


        }


        if (GUI.changed)
        {
            EditorUtility.SetDirty(setting);
        }
    }

}
