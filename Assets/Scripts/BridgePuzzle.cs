using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BridgePuzzle : MonoBehaviour
{
    public GameObject[] objectsToToggle;
    private Material[] changedState;
    private Material originalMaterial;
    private bool isPlayerNearby = false;
    private bool isPressed = false;

    public float toggleDuration = 10f; 
    private float toggleTimer = 0f;

    public TextMeshProUGUI timerUI;

    void Start()
    {
        // Initialize array and load materials from Resources
        changedState = new Material[2];

        changedState[0] = Resources.Load<Material>("Green");
        changedState[1] = Resources.Load<Material>("Red");

        // Store the original material of the object
        originalMaterial = GetComponent<Renderer>().material;

        if (timerUI != null)
        {
            timerUI.text = "";
        }
    }

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            isPressed = !isPressed;

            int materialIndex = isPressed ? 1 : 0;
            Material materialToApply = changedState[materialIndex];

            toggleTimer = toggleDuration;

            GetComponent<Renderer>().material = materialToApply;
            ToggleObjects(isPressed);
        }

        if (toggleTimer > 0f)
        {
            toggleTimer -= Time.deltaTime;

            if (timerUI != null)
            {
                timerUI.text = "Time: " + Mathf.Ceil(toggleTimer).ToString() + "s";
            }

            if (toggleTimer <= 0f)
            {
                // Revert Everything to original state
                isPressed = false;
                ToggleObjects(false);

                Material[] materials = GetComponent<Renderer>().materials;
                materials[0] = originalMaterial;
                GetComponent<Renderer>().materials = materials;

                if (timerUI != null)
                {
                    timerUI.text = "";
                }
            }
        }
    }

    void ToggleObjects(bool enable)
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(enable);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
