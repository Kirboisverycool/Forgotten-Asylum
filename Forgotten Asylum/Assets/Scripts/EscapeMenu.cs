using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EscapeMenu : MonoBehaviour
{
    [SerializeField] GameObject menuParent;
    PlayerMouvement player;
    public bool isPaused;
    [SerializeField]AudioSource normalAudio;
    [SerializeField]AudioSource menuAudio;
    [SerializeField] string menuSceneName;
    void Start()
    {
        player = FindObjectOfType<PlayerMouvement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuParent.SetActive(true);
            FindObjectOfType<PostProccesingInteracting>().ToggleBlur(true);
            player.GetComponent<PlayerMouvement>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            isPaused = true;
            normalAudio.mute = true;
            menuAudio.mute = false;
        }
    }
    public void CloseMenu()
    {
        menuParent.SetActive(false);
        FindObjectOfType<PostProccesingInteracting>().ToggleBlur(false);
        FindObjectOfType<PlayerMouvement>().GetComponent<PlayerMouvement>().enabled = true;
        isPaused = false;
        normalAudio.mute = false;
        menuAudio.mute = true;
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }
}
