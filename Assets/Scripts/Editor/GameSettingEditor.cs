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

        setting.fireRatio = EditorGUILayout.Slider("불속성 비율", setting.fireRatio, 0f, 1f);
        setting.waterRatio = EditorGUILayout.Slider("물속성 비율", setting.waterRatio, 0f, 1f);
        setting.grassRatio = EditorGUILayout.Slider("풀속성 비율", setting.grassRatio, 0f, 1f);

        float totalProbability = 1f;
        

        if (EditorGUI.EndChangeCheck())
        {
            float ratio = totalProbability / (setting.fireRatio + setting.waterRatio + setting.grassRatio);

            if(ratio > 0f)
            {
                setting.fireRatio *= ratio;
                setting.waterRatio *= ratio;
                setting.grassRatio *= ratio;
            }

            serializedObject.ApplyModifiedProperties();
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(setting);
        }
    }
}
