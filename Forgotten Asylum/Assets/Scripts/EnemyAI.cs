using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform target;
    Rigidbody2D rb;

    Vector2 moveDir;

    [SerializeField] float speed;
    [SerializeField] float updatePathTime;
    [SerializeField] float lineOfSite;

    float distanceFromPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        if(distanceFromPlayer < lineOfSite)
        {
            InvokeRepeating("UpdatePath", 0f, updatePathTime);
        }
    }

    void UpdatePath()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        moveDir = direction;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir.x, moveDir.y) * speed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }
}
