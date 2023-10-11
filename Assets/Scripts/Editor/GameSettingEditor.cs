using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        EditorGUILayout.LabelField("�� ������", EditorStyles.boldLabel);
        setting.GridMapWidth = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapHeight, 1, 10);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��Ʈ���� ���� �Ӽ�", EditorStyles.boldLabel);
        setting.mixBlockEnable = EditorGUILayout.Toggle("�Ӽ� ȥ�� Ȱ��ȭ", setting.mixBlockEnable);
        setting.singleElementRatio = EditorGUILayout.Slider("���� �Ӽ� ����", setting.singleElementRatio, 0f, 1f);
        setting.doubleElementRatio = EditorGUILayout.Slider("ȥ�� �Ӽ� ����", setting.doubleElementRatio, 0f, 1f);


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
