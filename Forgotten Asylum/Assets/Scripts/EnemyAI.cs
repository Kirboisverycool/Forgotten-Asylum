using System;
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

    [Header("Attack")]
    [SerializeField] float attackThrust;
    [SerializeField] float slowDown;
    [SerializeField] float attackDuration;
    [SerializeField] float attackDelay;
    [SerializeField] float timeToRecover;
    [SerializeField] float attackCooldown;
    bool isAttacking = false;
    float cooldownTimer;

    [Header("Collider")]
    [SerializeField] float colliderLength;
    [SerializeField] float colliderHeight;
    [SerializeField] float colliderDistance;
    [SerializeField] BoxCollider2D boxCollider;
    [SerializeField] LayerMask playerLayer;

    [Header("Movement")]
    [SerializeField] float speed;

    [Header("Target Location")]
    [SerializeField] float updatePathTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void UpdatePath()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        moveDir = direction;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdatePath();

        if(!isAttacking)
        {
            cooldownTimer += Time.deltaTime;
        }

        if(PlayerInSight())
        {
            if(!isAttacking && cooldownTimer >= attackCooldown)
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        rb.velocity = Vector3.zero;
        cooldownTimer = 0;
        float originalDrag = rb.drag;
        Vector3 currentTargetPosition = (target.position - transform.position).normalized;
        yield return new WaitForSeconds(attackDelay);
        rb.AddForce(currentTargetPosition * attackThrust, ForceMode2D.Impulse);
        yield return new WaitForSeconds(attackDuration);
        rb.drag = slowDown;
        yield return new WaitForSeconds(timeToRecover);
        rb.drag = originalDrag;
        isAttacking = false;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderDistance, new Vector3(boxCollider.bounds.size.x * colliderLength, boxCollider.bounds.size.y * colliderHeight, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveDir.x, moveDir.y) * speed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * colliderDistance, new Vector3(boxCollider.bounds.size.x * colliderLength, boxCollider.bounds.size.y * colliderHeight, boxCollider.bounds.size.z));
    }
}
