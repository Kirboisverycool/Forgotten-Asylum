using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;
using UnityEngine.Rendering;

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
    [SerializeField] float currentSpeed;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float slowDownTimer;

    [Header("Target Location")]
    [SerializeField] float updatePathTime;

    [Header("Animations")]
    Animator anim;
    Vector2 isMoving;

    [Header("Audio")]
    [SerializeField] float minVolume;
    [SerializeField] float maxVolume;
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] AudioSource aSource;

    private void Start()
    {
        currentSpeed = maxSpeed;

        anim = GetComponentInChildren<Animator>();
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
        if (!aSource.isPlaying)
        {
            aSource.volume = Random.Range(minVolume, maxVolume);
            aSource.pitch = Random.Range(minPitch, maxPitch);
            aSource.Play();
            //Debug.Log("audio");
        }

        isMoving = rb.velocity;
        FlipSprite();

        Animate();

        UpdatePath();

        if(!isAttacking)
        {
            cooldownTimer += Time.deltaTime;
        }

        if(PlayerInSight())
        {
            currentSpeed = minSpeed;
/*            if(!isAttacking && cooldownTimer >= attackCooldown)
            {
                StartCoroutine(Attack());
            }*/
        }
        else if(!PlayerInSight() && currentSpeed == minSpeed)
        {
            StartCoroutine(SpeedUp());
        }
    }

    private IEnumerator SpeedUp()
    {
        yield return new WaitForSeconds(slowDownTimer);
        currentSpeed = maxSpeed;
    }

    private void Animate()
    {
        anim.SetFloat("Speed", isMoving.sqrMagnitude);
        anim.SetBool("IsCharging", isAttacking);
    }

    private void FlipSprite()
    {
        if(rb.velocity.x < 0)
        {
            //spriteRenderer.flipX = true;
            transform.localScale = new Vector3(-1, 1, 1);
        }    
        else if(rb.velocity.x > 0)
        {
            //spriteRenderer.flipX = false;
            transform.localScale = Vector3.one;
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
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * colliderDistance * transform.localScale.x, new Vector3(boxCollider.bounds.size.x * colliderLength, boxCollider.bounds.size.y * colliderHeight, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(moveDir.x, moveDir.y) * currentSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * colliderDistance * transform.localScale.x, new Vector3(boxCollider.bounds.size.x * colliderLength, boxCollider.bounds.size.y * colliderHeight, boxCollider.bounds.size.z));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Jumpscare();
        }
    }

    private void Jumpscare()
    {
        SceneManager.LoadScene(3);
    }
}