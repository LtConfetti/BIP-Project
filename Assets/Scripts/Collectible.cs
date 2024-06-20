/**
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int pointsGained = 1;

    private SphereCollider sphereCollider;
    private Renderer visual;
    private bool isCollected = false;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        visual = GetComponentInChildren<Renderer>();
    }

    private void Collect()
    {
        if (isCollected) return;  // Prevent multiple collections

        isCollected = true;
        sphereCollider.enabled = false;
        visual.gameObject.SetActive(false);
        GameEventsManager.instance.goldEvents.GoldGained(pointsGained);
        GameEventsManager.instance.miscEvents.CoinCollected();
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            Collect();
        }
    }
}
*/
