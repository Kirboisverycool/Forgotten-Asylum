using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    EnemyAI enemy;
    [SerializeField] float distanceFromEnemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(distanceFromEnemy);
    }
}
