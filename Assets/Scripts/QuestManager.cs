using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int enemiesKilled;
    private int requiredKills = 2;
    private bool questActive;
    public bool questCompleted;
    private int currentQuestID = -1;
    public bool[] quests = new bool[8];

    public void StartQuest(int questID)
    {
        if (!quests[questID] && !questActive)
        {
            questActive = true;
            enemiesKilled = 0;
            currentQuestID = questID;
            questCompleted = false;
            Debug.Log($"Quest {questID} started: Kill {requiredKills} enemies.");
        }
        else if (quests[questID])
        {
            Debug.Log($"Quest {questID} already completed.");
        }
        else
        {
            Debug.Log("You are already on a quest. Complete it first.");
        }
    }

    public void EnemyKilled()
    {
        if (questActive)
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

    public void CompleteQuest()
    {
        if (questCompleted && !quests[currentQuestID])
        {
            quests[currentQuestID] = true;
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
