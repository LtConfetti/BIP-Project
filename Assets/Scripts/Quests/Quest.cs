using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using UnityEditor;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    [System.Serializable]
    public struct Info
    {
        public string Name;
        public Sprite Icon;
        [TextArea]
        public string Description;
    }

    [Header("Info")]
    public Info Information;

    [System.Serializable]
    public struct Stat
    {
        public int Petition;
    }

    [Header("Reward")]
    public Stat Reward = new Stat { Petition = 1 };

    public bool Completed { get; private set; }
    public QuestCompletedEvent QuestCompleted;

    public abstract class QuestGoal : ScriptableObject
    {
        protected string Description;
        public int CurrentAmount { get; protected set; }
        public int RequiredAmount = 1;
        public bool Completed { get; protected set; }
        [HideInInspector]
        public UnityEvent GoalCompleted;

        public virtual string GetDescription()
        {
            return Description;
        }

        public virtual void Initialize()
        {
            Completed = false;
            GoalCompleted = new UnityEvent();
        }

        protected void Complete()
        {
            Completed = true;
            GoalCompleted.Invoke();
            GoalCompleted.RemoveAllListeners();
        }

        public void Skip()
        {
            Complete();
        }
    }

    public List<QuestGoal> Goals = new List<QuestGoal>();

    public void Initialize()
    {
        Completed = false;
        QuestCompleted = new QuestCompletedEvent();

        foreach (var goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
        }
    }

    public void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
        if (Completed)
        {
            QuestCompleted.Invoke(this);
            QuestCompleted.RemoveAllListeners();
        }
    }
}

public class QuestCompletedEvent : UnityEvent<Quest> { }

#if UNITY_EDITOR
[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    SerializedProperty m_QuestInfoProperty;
    SerializedProperty m_QuestStatProperty;
    List<System.Type> m_QuestGoalType;
    SerializedProperty m_QuestGoalListProperty;

    [MenuItem("Assets/Create/Quest", priority = 0)]
    public static void CreateQuest()
    {
        var newQuest = CreateInstance<Quest>();
        ProjectWindowUtil.CreateAsset(newQuest, "New Quest.asset");
    }

    void OnEnable()
    {
        m_QuestInfoProperty = serializedObject.FindProperty("Information");
        m_QuestStatProperty = serializedObject.FindProperty("Reward");
        m_QuestGoalListProperty = serializedObject.FindProperty("Goals");

        var lookup = typeof(Quest.QuestGoal);
        m_QuestGoalType = System.AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(lookup))
            .ToList();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Display Quest Info
        EditorGUILayout.LabelField("Quest Info", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_QuestInfoProperty, true);

        // Display Quest Reward
        EditorGUILayout.LabelField("Quest Reward", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(m_QuestStatProperty, true);

        // Add new Quest Goal
        int choice = EditorGUILayout.Popup("Add new Quest Goal", -1, m_QuestGoalType.Select(type => type.Name).ToArray());

        if (choice != -1)
        {
            var newInstance = ScriptableObject.CreateInstance(m_QuestGoalType[choice]);
            AssetDatabase.AddObjectToAsset(newInstance, target);
            AssetDatabase.SaveAssets();

            m_QuestGoalListProperty.InsertArrayElementAtIndex(m_QuestGoalListProperty.arraySize);
            m_QuestGoalListProperty.GetArrayElementAtIndex(m_QuestGoalListProperty.arraySize - 1)
                .objectReferenceValue = newInstance;

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        // Display and manage Quest Goals
        for (int i = 0; i < m_QuestGoalListProperty.arraySize; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            SerializedProperty goalProperty = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
            Editor editor = Editor.CreateEditor(goalProperty.objectReferenceValue);
            editor.OnInspectorGUI();

            EditorGUILayout.EndVertical();

            if (GUILayout.Button("-", GUILayout.Width(32)))
            {
                var element = m_QuestGoalListProperty.GetArrayElementAtIndex(i);
                DestroyImmediate(element.objectReferenceValue, true);
                m_QuestGoalListProperty.DeleteArrayElementAtIndex(i);
                m_QuestGoalListProperty.DeleteArrayElementAtIndex(i);
                break;
            }

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
