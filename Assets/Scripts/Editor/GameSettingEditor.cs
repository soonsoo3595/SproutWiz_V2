using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        EditorGUILayout.LabelField("���� ����", EditorStyles.boldLabel);
        setting.GridMapWidth = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapHeight, 1, 10);

        setting.timeLimit = EditorGUILayout.IntField("�ð� ����", setting.timeLimit);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��Ʈ���� ���� �Ӽ�", EditorStyles.boldLabel);
        setting.mixBlockEnable = EditorGUILayout.Toggle("�Ӽ� ȥ�� Ȱ��ȭ", setting.mixBlockEnable);
        setting.singleElementRatio = EditorGUILayout.Slider("���� �Ӽ� ����", setting.singleElementRatio, 0f, 1f);
        setting.doubleElementRatio = EditorGUILayout.Slider("ȥ�� �Ӽ� ����", setting.doubleElementRatio, 0f, 1f);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("���� ����Ʈ ����", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("�Ӽ� ����", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("���� ����ġ", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("Ȯ��", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("���� ����ġ", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("Ȯ��", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("���� �Ӽ�", GUILayout.Width(100));
        setting.equal.growPoint = EditorGUILayout.IntField(setting.equal.growPoint, GUILayout.Width(100));
        setting.equal.percentage = EditorGUILayout.FloatField(setting.equal.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("���� �Ӽ�", GUILayout.Width(100));
        setting.irrelevant.growPoint = EditorGUILayout.IntField(setting.irrelevant.growPoint, GUILayout.Width(100));
        setting.irrelevant.percentage = EditorGUILayout.FloatField(setting.irrelevant.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("��� �Ӽ�", GUILayout.Width(100));
        setting.disadvantage.growPoint = EditorGUILayout.IntField(setting.disadvantage.growPoint, GUILayout.Width(100));
        setting.disadvantage.percentage = EditorGUILayout.FloatField(setting.disadvantage.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

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

        if (setting.timeLimit < 0)
        {
            EditorGUILayout.HelpBox("�ð� ������ 0���� Ŀ�� �մϴ�.", MessageType.Warning);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(setting);
        }
    }
}
