using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public enum QuestType
    {
        Kill,
        Fetch,
        KillFetch
    }
    public bool questActive;
    public bool questCompleted;
    public int currentQuestID = -1;
    public QuestType currentQuestType;
    public bool[] quests = new bool[8];

    private int enemiesKilled;
    public int requiredKills = 2;

    private int objectsCollected;
    public int requiredFetches = 1;
    public GameObject[] fetchableObjects;

    public void StartQuest(int questID, QuestType questType)
    {
        if (!quests[questID])
        {
            questActive = true;
            questCompleted = false;
            currentQuestID = questID;
            currentQuestType = questType;

            if (questType == QuestType.Kill)
            {
                enemiesKilled = 0;
                Debug.Log($"Quest {questID} started: Kill {requiredKills} enemies.");
            }
            else if (questType == QuestType.Fetch)
            {
                objectsCollected = 0;
                foreach (GameObject obj in fetchableObjects)
                {
                    obj.SetActive(true);
                }
                Debug.Log($"Quest {questID} started: Collect {requiredFetches} items.");
            }
            else if (questType == QuestType.KillFetch)
            {
                enemiesKilled = 0;
                Debug.Log($"Quest {questID} started: Kill {requiredKills} enemies.");
                objectsCollected = 0;
                foreach (GameObject obj in fetchableObjects)
                {
                    obj.SetActive(true);
                }
                Debug.Log($"Quest {questID} started: Collect {requiredFetches} items.");
            }
        }
        else
        {
            Debug.Log($"Quest {questID} already completed.");
        }
    }

    public void EnemyKilled()
    {
        if (questActive && currentQuestType == QuestType.Kill)
        {
            enemiesKilled++;
            Debug.Log("Enemy killed. Total kills: " + enemiesKilled);

            if (enemiesKilled >= requiredKills)
            {
                questActive = false;
                questCompleted = true;
                Debug.Log($"Quest {currentQuestID} requirements met. Return to NPC to complete.");
            }
        }
    }

    public void ObjectCollected()
    {
        if (questActive && currentQuestType == QuestType.Fetch)
        {
            objectsCollected++;
            Debug.Log("Object collected. Total collected: " + objectsCollected);

            if (objectsCollected >= requiredFetches)
            {
                questActive = false;
                questCompleted = true;
                Debug.Log($"Quest {currentQuestID} requirements met. Return to NPC to complete.");
            }
        }
    }

    public void CompleteQuest()
    {
        if (questCompleted && !quests[currentQuestID])
        {
            quests[currentQuestID] = true; // Mark the quest as completed
            questCompleted = false;
            Debug.Log($"Quest {currentQuestID} completed successfully.");
        }
        else if (quests[currentQuestID])
        {
            Debug.Log($"Quest {currentQuestID} has already been completed.");
        }
        else
        {
            Debug.Log("You need to fulfill the quest requirements first.");
        }
    }
}
