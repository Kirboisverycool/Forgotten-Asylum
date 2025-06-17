using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject player;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] float lookAtTimer;
    DoorManager doorManager;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        doorManager = FindObjectOfType<DoorManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.SetActive(true);
            StartCoroutine(EnemyShowcase());
        }
    }

    private IEnumerator EnemyShowcase()
    {
        boxCollider.enabled = false;
        player.GetComponent<PlayerMouvement>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        enemy.GetComponent<EnemyAI>().enabled = false;
        enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        FindObjectOfType<CinemachineVirtualCamera>().Follow = enemy.transform;
        //FindObjectOfType<CinemachineVirtualCamera>().LookAt = enemy.transform;
        yield return new WaitForSeconds(lookAtTimer);
        //FindObjectOfType<CinemachineVirtualCamera>().LookAt = player.transform;
        FindObjectOfType<CinemachineVirtualCamera>().Follow = player.transform;
        player.GetComponent<PlayerMouvement>().enabled = true;
        enemy.GetComponent<EnemyAI>().enabled = true;
    }
}
