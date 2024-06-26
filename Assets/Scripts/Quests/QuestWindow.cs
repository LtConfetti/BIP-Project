using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private Transform goalContent;
    [SerializeField] private TextMeshProUGUI petitionText;
    public void Initialize(Quest quest)
    {
        titleText.text = quest.Information.Name;
        descriptionText.text = quest.Information.Description;

        foreach (var goal in quest.Goals)
        {
            GameObject goalObj = Instantiate(goalPrefab, goalContent);
            goalObj.transform.Find("Text").GetComponent<Text>().text = goal.GetDescription();

            GameObject countObj = goalObj.transform.Find("Count").gameObject;
            GameObject skipObj = goalObj.transform.Find("Skip").gameObject;

            if (goal.Completed)
            {
                countObj.SetActive(false);
                skipObj.SetActive(false);
                goalObj.transform.Find("Done").gameObject.SetActive(true);
            }
            else
            {
                countObj.GetComponent<Text>().text = goal.CurrentAmount + "/" + goal.RequiredAmount;
                skipObj.GetComponent<Button>().onClick.AddListener(delegate
                {
                    goal.Skip();

                    countObj.SetActive(false);
                    skipObj.SetActive(false);
                    goalObj.transform.Find("Done").gameObject.SetActive(true);
                });
            }

        }
        petitionText.text = quest.Reward.Petition.ToString();


    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < goalContent.childCount; i++)
        {
            Destroy(goalContent.GetChild(i).gameObject);
        }

    }
}
