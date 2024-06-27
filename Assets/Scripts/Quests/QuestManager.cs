using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

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

    // TextMeshProUGUI elements
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI questStatusText;
    public TextMeshProUGUI completedQuestsText;
    public Image progress;
    private float timer = 0f;
    private float displayValue = 0f;

    private void Start()
    {
        UpdateCompletedQuestsUI();
        progress.fillAmount = 0.0f;
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

            questDescriptionText.text = $"{questDescription}";
            questStatusText.text = "In Progress";
            //Debug.Log($"Quest {questID} started: {questDescription}");
        }
        else
        {
            questStatusText.text = "Quest already completed";
            Debug.Log($"Quest {questID} already completed.");
        }
    }

    public void EnemyKilled()
    {
        if (questActive && (currentQuestType == QuestType.Kill || currentQuestType == QuestType.KillFetch))
        {
            enemiesKilled++;
            //Debug.Log("Enemy killed. Total kills: " + enemiesKilled);

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
            //Debug.Log("Object collected. Total collected: " + objectsCollected);

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
            //Debug.Log($"Quest {currentQuestID} requirements met. Return to NPC to complete.");
        }
    }

    public void CompleteQuest()
    {
        if (questCompleted && !quests[currentQuestID])
        {
            quests[currentQuestID] = true; // Mark the quest as completed
            questCompleted = false;

            questDescriptionText.text = "";
            questStatusText.text = "";
            UpdateCompletedQuestsUI();
            //Debug.Log($"Quest {currentQuestID} completed successfully.");
        }
        else if (quests[currentQuestID])
        {
            Debug.Log($"Quest {currentQuestID} has already been completed.");
            questStatusText.text = "Quest already completed";
        }
        else
        {
            Debug.Log("You need to fulfill the quest requirements first.");
            questStatusText.text = "You need to fulfill the quest requirements first";
        }
    }

    private void UpdateCompletedQuestsUI()
    {
        int completedCount = 0;
        foreach (bool quest in quests)
        {
            if (quest) completedCount++;
        }

        completedQuestsText.text = $"{completedCount}/{quests.Length} signatures";

        // Start the coroutine to animate the progress bar
        StartCoroutine(AnimateProgress(completedCount));
    }

    private IEnumerator AnimateProgress(int completedCount)
    {
        float targetFillAmount = completedCount * 0.125f;
        float startFillAmount = progress.fillAmount;
        float duration = 1.0f; // Duration of the animation
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            progress.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsed / duration);
            yield return null;
        }

        progress.fillAmount = targetFillAmount;
    }
}
