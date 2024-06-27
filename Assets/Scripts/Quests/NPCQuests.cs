using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;

public class NPCQuests : MonoBehaviour
{

    private bool playerInRange;
    public QuestManager questManager;
    public int questID; // ID of the quest this NPC starts
    public QuestManager.QuestType questType; // Type of the quest
    public GameObject enemies;
    public GameObject[] fetches;

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

                if(questType == QuestType.Kill)
                {
                    Spawner(enemies);
                }
                else if(questType == QuestType.Fetch)
                {
                    Spawner(fetches[0]);
                    Spawner(fetches[1]);
                }
               
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
    private void Spawner(GameObject thing)
    {
        thing.SetActive(true);
    }
}
