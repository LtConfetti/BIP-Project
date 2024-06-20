using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "ScriptableObjects/QuestInfoSO", order = 1)]
public class QuestInfoSO : ScriptableObject
{

    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;

    [Header("Requirement")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefab;

    [Header("Rewards")]
    public int completeReward;

        //Used to make ID the same as the name of the Scripted Object
        private void OnValidate()
    {
      #if UNITY_EDITOR
      id = this.name;
      UnityEditor.EditorUtility.SetDirty(this);
      #endif
    }
}
