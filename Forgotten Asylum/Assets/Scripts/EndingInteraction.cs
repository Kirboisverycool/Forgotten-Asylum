using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingInteraction : MonoBehaviour
{
    [SerializeField] GameObject parentOfText;
    [SerializeField] GameObject endFadeObj;
    [SerializeField] string sceneName;
    bool inRange = false;
    void Start()
    {
        parentOfText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            FindObjectOfType<PlayerMouvement>().enabled = false;
            FindObjectOfType<PlayerMouvement>().GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            StartCoroutine(WaitForAnimation());
            endFadeObj.SetActive(true);
        }
    }
    IEnumerator WaitForAnimation()
    { 
        yield return new WaitForSeconds(14);
        SceneManager.LoadScene(sceneName);  

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        parentOfText.SetActive(true);
        inRange = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        parentOfText.SetActive(false);
        inRange = false;
    }
}
