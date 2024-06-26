using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.TimeZoneInfo;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public void playGame()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        transition.SetTrigger("Start");
        Debug.Log("Loading scene");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Hub");
    }
    //ends the game
    public void quitGame()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
