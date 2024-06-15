using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInScene : MonoBehaviour
{
    //Code from https://www.youtube.com/watch?v=2IDrPmGf7Mg
    public Transform player, destination;
    public GameObject playerg;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { }
        {
            playerg.SetActive(false);
            player.position = destination.position;
            playerg.SetActive(true);
        }
    }
}
