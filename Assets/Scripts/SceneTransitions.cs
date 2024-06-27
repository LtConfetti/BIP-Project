using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    public string level;
    public Animator transition;
    public float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenScene();
    }
    public void OpenScene()
    {
        StartCoroutine(LoadScene(level));
    }
    IEnumerator LoadScene(string level)
    {
        transition.SetTrigger("Start");
        Debug.Log("Loading scene");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }
}
