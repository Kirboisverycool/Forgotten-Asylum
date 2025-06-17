using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] string mainSceneName;
    [SerializeField] float timer;
    [SerializeField] GameObject art;
    [SerializeField] GameObject music;
    [SerializeField] AudioSource aSource;
    [SerializeField] Canvas canvas;

    [SerializeField] bool isLoseScreen = false;

    private void Start()
    {
        if(isLoseScreen)
        {
            StartCoroutine(JumpScare());
        }
    }

    private IEnumerator JumpScare()
    {
        aSource.Play();
        yield return new WaitForSeconds(timer);
        art.SetActive(false);
        music.SetActive(true);
        canvas.gameObject.SetActive(true);
        
    }

    public void LoadMainGame()
    {
        StartCoroutine(GameLoadDelay());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
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
