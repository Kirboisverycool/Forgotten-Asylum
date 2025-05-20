using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//using UnityEngine.Windows;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class PlayerMouvement : MonoBehaviour
{
    [Header("Movement")]
    Rigidbody2D rb;
    Vector2 movementInputs;

    [Header("Puzzle")]
    [SerializeField] bool isDoingPuzzle = false;
    [SerializeField] GameObject[] puzzles;

    [Header("Speeds")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintMultiplier = 1f;

    [Header("Stamina")]
    [SerializeField] float stamina = 100f;
    [SerializeField] float removeStaminaRate= 0.1f;
    [SerializeField] float addStaminaCoolDown = 3;
    [SerializeField] float timeSinceLastSprinted = 0;
    [SerializeField] bool isMoving = false;
    bool isRemovingStamina = false;
    bool isAddingStamina = false;

    [Header("UI")]
    [SerializeField] GameObject staminaBar;
    Image staminaFillBar;


    [Header("Sounds")]
    [SerializeField] AudioClip groundSoundClip;
    AudioSource aSource;

    [Header("Animations")]
    Animator anim;
    Vector2 lastMoveDirection;


    public void GroundSoundChange(AudioClip audioClip)
    {
        groundSoundClip = audioClip;
        aSource.clip = groundSoundClip;
    }

    void Start()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;


     

        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        staminaBar.SetActive(false);
        staminaFillBar = staminaBar.transform.GetChild(1).GetComponent<Image>();
    }

 
    void Update()
    {


        if (!isDoingPuzzle)
        {
            MouvementInputs();
            Sprint();
            MoveAudio();
            Animate();
        }

        StopIfDoingPuzzle();

        Mathf.Clamp(stamina, 0, 100);

        timeSinceLastSprinted += Time.deltaTime;
        if (!isRemovingStamina && stamina < 100 && timeSinceLastSprinted > 3)
        {
            if (!isAddingStamina)
            {
                StartCoroutine(AddStamina());
            }
        }
        if (!isRemovingStamina && stamina >= 100)
        {
            staminaBar.SetActive(false);
        }
    }

    private void StopIfDoingPuzzle()
    {
        if (puzzles != null)
        {
            for (int i = 0; i < puzzles.Length; i++)
            {
                if (puzzles[i].activeInHierarchy)
                {
                    isDoingPuzzle = true;
                    break;
                }
                else
                {
                    isDoingPuzzle = false;
                }
            }
        }
    }

    private void Animate()
    {
        anim.SetFloat("Horizontal", movementInputs.x);
        anim.SetFloat("Vertical", movementInputs.y);
        anim.SetFloat("Speed", movementInputs.sqrMagnitude);
        anim.SetFloat("LastHorizontal", lastMoveDirection.x);
        anim.SetFloat("LastVertical", lastMoveDirection.y);
    }

    private void MoveAudio()
    {
        if (rb.velocity.magnitude > 0)
        {
            if (!aSource.isPlaying)
            {
                aSource.Play();
                //Debug.Log("audio");
            }
        }
        else
        {
            aSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        if (!isDoingPuzzle)
        {
            Move();
        }
    }

    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && isMoving)
        {
            staminaBar.SetActive(true);
            sprintMultiplier = stamina/100 + 1;
            if (!isRemovingStamina)
            {   
                StartCoroutine(RemoveStamina());
            }
        } 
        else 
        { 
            sprintMultiplier = 1f;// staminaBar.SetActive(false);
        }
    
    }

    private void UpdateStaminaBar()
    {
        float currentFillAmmount = staminaFillBar.fillAmount;
        float fillAmmount = stamina / 100;


        staminaFillBar.fillAmount = fillAmmount; 
            //Mathf.Lerp(currentFillAmmount, fillAmmount, 0.1f);
    }

    IEnumerator RemoveStamina()
    {
        timeSinceLastSprinted = 0;
        isRemovingStamina=true;
        stamina -= 0.5f;
        
        UpdateStaminaBar();
        yield return new WaitForSeconds(removeStaminaRate);
        isRemovingStamina=false;
    }

    IEnumerator AddStamina()
    {
        isAddingStamina=true;
        stamina += 0.3f;
      
        UpdateStaminaBar();
        yield return new WaitForSeconds(removeStaminaRate);
        isAddingStamina = false;
    }

    private void MouvementInputs()
    {
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        if ((InputX == 0 && InputY == 0) && (movementInputs.x != 0 || movementInputs.y != 0))
        {
            lastMoveDirection = movementInputs;
        }

        if(movementInputs.sqrMagnitude > 0.01)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }    

        movementInputs = new Vector2(InputX, InputY).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(movementInputs.x * movementSpeed * sprintMultiplier, movementInputs.y * movementSpeed * sprintMultiplier);
    }
}