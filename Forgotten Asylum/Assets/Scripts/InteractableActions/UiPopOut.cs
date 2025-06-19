using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPopOut : MonoBehaviour
{
    [SerializeField] GameObject ui;

    [SerializeField] GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        if (GameObject.FindGameObjectsWithTag("UiPopUp").Length > 0)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            FindObjectOfType<PostProccesingInteracting>().ToggleBlur(true);
            var uiIrl = Instantiate(ui);
            uiIrl.transform.SetParent(GameObject.FindWithTag("MainCanvas").transform, false);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            player.GetComponent<PlayerMouvement>().enabled = false;
            player.GetComponent<AudioSource>().Pause();
            gameObject.SetActive(false);
            return;
        }
      

      

    }
    
}
