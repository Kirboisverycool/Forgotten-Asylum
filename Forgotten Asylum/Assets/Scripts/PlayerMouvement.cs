using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;

public class PlayerMouvement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movementInputs;

    [Header("Speeds")]
    [SerializeField] float movementSpeed;
    [SerializeField] float sprintMultiplier = 1f;

    [Header("Stamina")]
    [SerializeField] float stamina = 100f;
    [SerializeField] float removeStaminaRate= 0.1f;
    [SerializeField] float addStaminaCoolDown = 3;
    [SerializeField] float timeSinceLastSprinted = 0;
    bool isRemovingStamina = false;
    bool isAddingStamina = false;

    [Header("UI")]
    [SerializeField] GameObject staminaBar;
    Image staminaFillBar;


    [Header("Sounds")]
    [SerializeField] AudioClip groundSoundClip;
    AudioSource aSource;

    [Header("Animations")]
    [SerializeField] string boolFoward;
    [SerializeField] string boolBackward;
    [SerializeField] string boolLeft;
    [SerializeField] string boolRight;
    Animator anim;
    public void GroundSoundChange(AudioClip audioClip)
    {
        groundSoundClip = audioClip;
        aSource.clip = groundSoundClip;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        staminaBar.SetActive(false);
        staminaFillBar = staminaBar.transform.GetChild(1).GetComponent<Image>();
    }

 
    void Update()
    {
        MouvementInputs();
        Sprint();
        MoveAudio();
        MouvementDir();
        

        Mathf.Clamp(stamina, 0, 100);

        timeSinceLastSprinted += Time.deltaTime;
        if (!isRemovingStamina && stamina < 100 && timeSinceLastSprinted > 3)
        {
            if (!isAddingStamina)
            {
                StartCoroutine(AddStamina());
            }
        }

    }
    private void Animation(string s, bool state)
    {
        anim.SetBool(s, state);                  
    }
    private void MouvementDir()
    {
        float x = rb.velocity.x;
        float y = rb.velocity.y;

        if (x < 0) { Animation(boolLeft,true); }
        else if (anim.GetBool(boolLeft) && x>=0) { Animation(boolLeft, false); }

        if (x > 0) { Animation(boolRight,true); }
        else if (anim.GetBool(boolRight) && x <= 0) { Animation(boolRight, false); }

        if (y > 0) { Animation(boolBackward, true); }
        else if (anim.GetBool(boolBackward) && y <= 0) { Animation(boolBackward, false); }

            if (y < 0) { Animation(boolFoward,true); }
        else if (anim.GetBool(boolFoward) && y >= 0) { Animation(boolFoward, false); }
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
        Move();
    }
    private void Sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
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

        anim.SetFloat("Horizontal", InputX);
        anim.SetFloat("Vertical", InputY);
        anim.SetFloat("Speed", movementInputs.sqrMagnitude);

        movementInputs = new Vector2(InputX, InputY).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(movementInputs.x * movementSpeed * sprintMultiplier, movementInputs.y * movementSpeed * sprintMultiplier);

    }
}
