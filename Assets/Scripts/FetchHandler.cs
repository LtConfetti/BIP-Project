using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FetchHandler : MonoBehaviour
{

    private bool playerInRange;
    public QuestManager questManager;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectObject();
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

    private void CollectObject()
    {
        //questManager.ObjectCollected();
        gameObject.SetActive(false); // Make the object invisible
        Debug.Log("Object collected and made invisible.");
    }
}
