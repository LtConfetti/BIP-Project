using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private int enemiesKilled;
    private int requiredKills = 2;
    private bool questActive;
    public bool questCompleted;
    private int currentQuestID = -1;
    public bool[] quests = new bool[8];
    public FetchHandler fetchObj;

    public void StartQuest(int questID, string questType)
    {
        if (!quests[questID] && !questActive)
        {
            questActive = true;
            if (questType == "kill")
            {
                Kill(questID);
            }
            else if (questType == "fetch")
            {
                Fetch(questID);
            }
            else if (questType == "fetchkill")
            {
                FetchKill(questID);
            }
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

    public void Fetch(int questID)
    {

    }

    public void Kill(int questID)
    {
        enemiesKilled = 0;
        currentQuestID = questID;
        questCompleted = false;
        Debug.Log($"Quest {questID} started: Kill {requiredKills} enemies.");
    }
    public void FetchKill(int questID)
    {
        enemiesKilled = 0;
        currentQuestID = questID;
        questCompleted = false;
        Debug.Log($"Quest {questID} started: Kill {requiredKills} enemies.");

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
