using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.SceneManagement;

public class TeleportInScene : MonoBehaviour
{
    //Code from https://www.youtube.com/watch?v=2IDrPmGf7Mg
    public Transform player, destination;
    public GameObject playerg;

    public Animator transition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { }
        {
            StartCoroutine(LoadScene());
        }
    }
    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        Debug.Log("Loading scene");
        Debug.Log("triger0");
        yield return new WaitForSeconds(4);
        Debug.Log("triger1");
        playerg.SetActive(false);
        player.position = destination.position;
        Debug.Log("triger2");
        playerg.SetActive(true);
    }
}
