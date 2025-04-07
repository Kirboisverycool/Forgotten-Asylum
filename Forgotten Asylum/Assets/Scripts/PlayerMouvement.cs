using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMouvement : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 movementInputs;

    [SerializeField] float movementSpeed;
    [SerializeField] float sprintMultiplier = 1f;

    [SerializeField] float stamina = 100f;
    [SerializeField] float removeStaminaRate= 0.1f;
    [SerializeField] float addStaminaCoolDown = 3;
    [SerializeField] float timeSinceLastSprinted = 0;

    [SerializeField] GameObject staminaBar;
    Image staminaFillBar;
    
    bool isRemovingStamina = false;
    bool isAddingStamina = false;
   
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
        staminaBar.SetActive(false);
        staminaFillBar = staminaBar.transform.GetChild(1).GetComponent<Image>();
    }

 
    void Update()
    {
        MouvementInputs();
        Sprint();

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
   
   
        staminaFillBar.fillAmount = Mathf.Lerp(currentFillAmmount, fillAmmount, 0.5f);
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

        movementInputs = new Vector2(InputX, InputY).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(movementInputs.x * movementSpeed * sprintMultiplier, movementInputs.y * movementSpeed * sprintMultiplier);

    }
}
