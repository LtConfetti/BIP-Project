using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuests : MonoBehaviour
{

    private bool playerInRange;
    public QuestManager questManager;
    public int questID; // ID of the quest this NPC starts
    public QuestManager.QuestType questType; // Type of the quest

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (questManager.questCompleted)
            {
                questManager.CompleteQuest();
            }
            else if (!questManager.questActive)
            {
                questManager.StartQuest(questID, questType);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
