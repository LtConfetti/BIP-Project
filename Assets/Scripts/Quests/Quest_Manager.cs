using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest_Manager : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private Transform questContent;
    [SerializeField] private GameObject questHolder;

    public List<Quest> CurrentQuests;

    public void Awake()
    {
        foreach (var quest in CurrentQuests)
        {
            quest.Initialize();
            quest.QuestCompleted.AddListener(OnQuestCompleted);

            GameObject questObj = Instantiate(questPrefab, questContent);
            questObj.transform.Find("Icon").GetComponent<Image>().sprite = quest.Information.Icon;
            questObj.GetComponent<Button>().onClick.AddListener(delegate
            {
                questHolder.GetComponent<QuestWindow>().Initialize(quest);
                questHolder.SetActive(true);
            }
            );
        }
    }
    private void OnQuestCompleted(Quest quest)
    {
        questContent.GetChild(CurrentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
    }

}
