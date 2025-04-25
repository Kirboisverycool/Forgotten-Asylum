using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseTrigger : MonoBehaviour
{
    [SerializeField] GameObject enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            enemy.SetActive(true);
        }
    }
}
