using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI questStatusText;
    public GameObject completedQuestsPanel;
    public GameObject completedQuestTextPrefab;

    private void Start()
    {
        UpdateUI();
    }

    public void StartQuest(int questID, QuestType questType)
    {
        if (!quests[questID])
        {
            questActive = true;
            questCompleted = false;
            currentQuestID = questID;
            currentQuestType = questType;

            string questDescription = "";

            if (questType == QuestType.Kill)
            {
                enemiesKilled = 0;
                questDescription = $"Kill {requiredKills} enemies.";
            }
            else if (questType == QuestType.Fetch)
            {
                objectsCollected = 0;
                foreach (GameObject obj in fetchableObjects)
                {
                    obj.SetActive(true);
                }
                questDescription = $"Collect {requiredFetches} items.";
            }
            else if (questType == QuestType.KillFetch)
            {
                enemiesKilled = 0;
                objectsCollected = 0;
                foreach (GameObject obj in fetchableObjects)
                {
                    obj.SetActive(true);
                }
                questDescription = $"Kill {requiredKills} enemies and collect {requiredFetches} items.";
            }

            questDescriptionText.text = $"Quest {questID}: {questDescription}";
            questStatusText.text = "In Progress";
            Debug.Log($"Quest {questID} started: {questDescription}");
        }
        else
        {
            Debug.Log($"Quest {questID} already completed.");
        }
    }

    public void EnemyKilled()
    {
        if (questActive && (currentQuestType == QuestType.Kill || currentQuestType == QuestType.KillFetch))
        {
            enemiesKilled++;
            Debug.Log("Enemy killed. Total kills: " + enemiesKilled);

            if (enemiesKilled >= requiredKills)
            {
                CheckQuestCompletion();
            }
        }
    }

    public void ObjectCollected()
    {
        if (questActive && (currentQuestType == QuestType.Fetch || currentQuestType == QuestType.KillFetch))
        {
            objectsCollected++;
            Debug.Log("Object collected. Total collected: " + objectsCollected);

            if (objectsCollected >= requiredFetches)
            {
                CheckQuestCompletion();
            }
        }
    }

    private void CheckQuestCompletion()
    {
        if ((currentQuestType == QuestType.Kill && enemiesKilled >= requiredKills) ||
            (currentQuestType == QuestType.Fetch && objectsCollected >= requiredFetches) ||
            (currentQuestType == QuestType.KillFetch && enemiesKilled >= requiredKills && objectsCollected >= requiredFetches))
        {
            questActive = false;
            questCompleted = true;
            questStatusText.text = "Return to NPC to complete";
            Debug.Log($"Quest {currentQuestID} requirements met. Return to NPC to complete.");
        }
    }

    public void CompleteQuest()
    {
        if (questCompleted && !quests[currentQuestID])
        {
            quests[currentQuestID] = true; // Mark the quest as completed
            questCompleted = false;

            // Add the quest to the completed quests UI
            GameObject completedQuestText = Instantiate(completedQuestTextPrefab, completedQuestsPanel.transform);
            completedQuestText.GetComponent<TextMeshProUGUI>().text = $"Quest {currentQuestID} completed";

            questDescriptionText.text = "";
            questStatusText.text = "";
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

    private void UpdateUI()
    {
        // Initialize or update the UI elements
        if (!questActive)
        {
            questDescriptionText.text = "";
            questStatusText.text = "";
        }

        // Clear completed quests panel
        foreach (Transform child in completedQuestsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        // Add completed quests to the UI
        for (int i = 0; i < quests.Length; i++)
        {
            if (quests[i])
            {
                GameObject completedQuestText = Instantiate(completedQuestTextPrefab, completedQuestsPanel.transform);
                completedQuestText.GetComponent<TextMeshProUGUI>().text = $"Quest {i} completed";
            }
        }
    }
}
