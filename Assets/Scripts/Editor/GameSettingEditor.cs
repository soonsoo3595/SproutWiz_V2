using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
       //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        setting.GridMapWidth = EditorGUILayout.IntSlider("맵 가로 사이즈", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("맵 세로 사이즈", setting.GridMapHeight, 1, 10);

        setting.PercentageOfMixTetris = EditorGUILayout.Slider("혼합 테트리스 생성 확률", setting.PercentageOfMixTetris, 0, 100);
        setting.EnableMix_Three_Type = EditorGUILayout.Toggle("3속성 혼합 가능", setting.EnableMix_Three_Type);

        setting.probabilityA = EditorGUILayout.Slider("불 속성 생성 비율", setting.probabilityA, 0f, 100f);
        setting.probabilityB = EditorGUILayout.Slider("물 속성 생성 비율", setting.probabilityB, 0f, 100f);
        setting.probabilityC = EditorGUILayout.Slider("풀 속성 생성 비율", setting.probabilityC, 0f, 100f);


        setting.EnableMix_Two_Type = EditorGUILayout.Toggle("2속성 혼합 가능", setting.EnableMix_Two_Type);


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
