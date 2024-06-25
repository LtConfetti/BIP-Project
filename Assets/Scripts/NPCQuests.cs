using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCQuests : MonoBehaviour
{
    public GameObject objectToHideShow;
    private bool playerInRange;
    public QuestManager questManager;
    public int questID;
    public string questType;
    private void Start()
    {

    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (questManager.quests[questID] || questManager.questCompleted)
            {
                questManager.CompleteQuest();
            }
            else
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