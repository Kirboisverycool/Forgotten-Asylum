using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] string mainSceneName;
    public void LoadMainGame()
    {
        StartCoroutine(GameLoadDelay());
       
    }
    private IEnumerator GameLoadDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(mainSceneName);
    }

    public void ExitScene()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
