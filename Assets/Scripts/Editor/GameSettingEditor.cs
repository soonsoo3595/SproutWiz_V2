using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        EditorGUILayout.LabelField("���� ���� (���� �� ���� �Ұ�)", EditorStyles.boldLabel);
        setting.GridMapWidth = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("�� ���� ������", setting.GridMapHeight, 1, 10);

        setting.timeLimit = EditorGUILayout.IntField("�ð� ����", setting.timeLimit);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��ũ�� �̸����� ����", EditorStyles.boldLabel);
        setting.DistanceFromHand = EditorGUILayout.Slider("�հ� ��Ʈ���� �̰�", setting.DistanceFromHand, 0f, 1000f);
        setting.DistanceFromTetris_x = EditorGUILayout.Slider("�̹��� �̰� x", setting.DistanceFromTetris_x, -1000f, 1000f);
        setting.DistanceFromTetris_y = EditorGUILayout.Slider("�̹��� �̰� y", setting.DistanceFromTetris_y, -1000f, 1000f);

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


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("��ũ�� ���� Ȯ��", EditorStyles.boldLabel);
        for(int i = 0; i< 7; i++)
        {
            setting.tetrisSpawnSet.typeRatio[i] = EditorGUILayout.IntSlider($" {(char)('A'+ i)} Ÿ�� : ", setting.tetrisSpawnSet.typeRatio[i], 0, 100);
        }


        float totalProbability = 1f;
        
        if (EditorGUI.EndChangeCheck())
        {
            float ratio = totalProbability / (setting.singleElementRatio + setting.doubleElementRatio);

            if(ratio > 0f)
            {
                setting.singleElementRatio *= ratio;
                setting.doubleElementRatio *= ratio;
            }
           
            int ratioSum = 0;
            for (int i = 0; i < 7; i++)
            {
                ratioSum += setting.tetrisSpawnSet.typeRatio[i];
            }

            if (ratioSum != 100)
            {
                EditorGUILayout.HelpBox($"�� ���� 100�� �Ǿ�� �մϴ�. ����{ratioSum}", MessageType.Error);
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
