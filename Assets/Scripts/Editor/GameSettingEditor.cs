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

        setting.fireRatio = EditorGUILayout.Slider("�ҼӼ� ����", setting.fireRatio, 0f, 1f);
        setting.waterRatio = EditorGUILayout.Slider("���Ӽ� ����", setting.waterRatio, 0f, 1f);
        setting.grassRatio = EditorGUILayout.Slider("Ǯ�Ӽ� ����", setting.grassRatio, 0f, 1f);

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
