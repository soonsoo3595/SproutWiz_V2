using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        EditorGUILayout.LabelField("맵 사이즈", EditorStyles.boldLabel);
        setting.GridMapWidth = EditorGUILayout.IntSlider("맵 가로 사이즈", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("맵 세로 사이즈", setting.GridMapHeight, 1, 10);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("테트리스 스폰 속성", EditorStyles.boldLabel);
        setting.mixBlockEnable = EditorGUILayout.Toggle("속성 혼합 활성화", setting.mixBlockEnable);
        setting.singleElementRatio = EditorGUILayout.Slider("단일 속성 비율", setting.singleElementRatio, 0f, 1f);
        setting.doubleElementRatio = EditorGUILayout.Slider("혼합 속성 비율", setting.doubleElementRatio, 0f, 1f);


        float totalProbability = 1f;
        
        if (EditorGUI.EndChangeCheck())
        {
            float ratio = totalProbability / (setting.singleElementRatio + setting.doubleElementRatio);

            if(ratio > 0f)
            {
                setting.singleElementRatio *= ratio;
                setting.doubleElementRatio *= ratio;
            }

            serializedObject.ApplyModifiedProperties();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(setting);
        }
    }
}
