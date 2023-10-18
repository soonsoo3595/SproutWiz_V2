using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameSetting))]
public class GameSettingEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();

        GameSetting setting = (GameSetting)target;

        EditorGUILayout.LabelField("게임 세팅 (실행 중 변경 불가)", EditorStyles.boldLabel);
        setting.GridMapWidth = EditorGUILayout.IntSlider("맵 가로 사이즈", setting.GridMapWidth, 1, 10);
        setting.GridMapHeight = EditorGUILayout.IntSlider("맵 세로 사이즈", setting.GridMapHeight, 1, 10);

        setting.timeLimit = EditorGUILayout.IntField("시간 제한", setting.timeLimit);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스크롤 미리보기 세팅", EditorStyles.boldLabel);
        setting.DistanceFromHand = EditorGUILayout.Slider("손과 테트리스 이격", setting.DistanceFromHand, 0f, 1000f);
        setting.DistanceFromTetris_x = EditorGUILayout.Slider("이미지 이격 x", setting.DistanceFromTetris_x, -1000f, 1000f);
        setting.DistanceFromTetris_y = EditorGUILayout.Slider("이미지 이격 y", setting.DistanceFromTetris_y, -1000f, 1000f);

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("테트리스 스폰 속성", EditorStyles.boldLabel);
        setting.mixBlockEnable = EditorGUILayout.Toggle("속성 혼합 활성화", setting.mixBlockEnable);
        setting.singleElementRatio = EditorGUILayout.Slider("단일 속성 비율", setting.singleElementRatio, 0f, 1f);
        setting.doubleElementRatio = EditorGUILayout.Slider("혼합 속성 비율", setting.doubleElementRatio, 0f, 1f);


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("성장 포인트 설정", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("속성 관계", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("성장 변동치", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("확률", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("성장 변동치", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.LabelField("확률", EditorStyles.boldLabel, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("같은 속성", GUILayout.Width(100));
        setting.equal.growPoint = EditorGUILayout.IntField(setting.equal.growPoint, GUILayout.Width(100));
        setting.equal.percentage = EditorGUILayout.FloatField(setting.equal.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("무관 속성", GUILayout.Width(100));
        setting.irrelevant.growPoint = EditorGUILayout.IntField(setting.irrelevant.growPoint, GUILayout.Width(100));
        setting.irrelevant.percentage = EditorGUILayout.FloatField(setting.irrelevant.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("취약 속성", GUILayout.Width(100));
        setting.disadvantage.growPoint = EditorGUILayout.IntField(setting.disadvantage.growPoint, GUILayout.Width(100));
        setting.disadvantage.percentage = EditorGUILayout.FloatField(setting.disadvantage.percentage, GUILayout.Width(100));
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.Space();
        EditorGUILayout.LabelField("스크롤 스폰 확률", EditorStyles.boldLabel);
        for(int i = 0; i< 7; i++)
        {
            setting.tetrisSpawnSet.typeRatio[i] = EditorGUILayout.IntSlider($" {(char)('A'+ i)} 타입 : ", setting.tetrisSpawnSet.typeRatio[i], 0, 100);
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
                EditorGUILayout.HelpBox($"총 합이 100이 되어야 합니다. 현재{ratioSum}", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }

        if (setting.timeLimit < 0)
        {
            EditorGUILayout.HelpBox("시간 제한은 0보다 커야 합니다.", MessageType.Warning);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(setting);
        }
    }
}
