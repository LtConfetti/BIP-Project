using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Just a lil header for other devs it is meant to be HIDDEN as all this script does is make it appear from being invisible from the canvas

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel; //The Panel 
    public TextMeshProUGUI dialogueText; //Dialogue text
    public TextMeshProUGUI nameText; // Name text
    public Image characterPortrait; // Portrait image to place sprite into
    public string[] characterNames; // Array of character names for dialogue line
    public Sprite[] characterSprites; // Array of character portraits for dialogue line

    [TextArea(3, 10)]
    public string[] dialogue;
    private int index = 0;


    public float wordSpeed = 0.2f; //LOWER = FASTER TEXT
    public bool playerIsClose;

    void Start()
    {
        dialogueText.text = ""; //Clear text to ensure it is empty
        nameText.text = ""; // Clear character name
        characterPortrait.sprite = null; // Clear character portrait
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose) //Initiate Dialogue
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                UpdateCharacterInfo();
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index]) //Next line of dialogue
            {
                NextLine();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && dialoguePanel.activeInHierarchy) //Cancel Dialogue
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        nameText.text = ""; // Clear character name
        characterPortrait.sprite = null; // Clear character portrait
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            UpdateCharacterInfo();
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    void UpdateCharacterInfo()
    {
        nameText.text = characterNames[index]; // Set character name
        characterPortrait.sprite = characterSprites[index]; // Set character portrait
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
}
