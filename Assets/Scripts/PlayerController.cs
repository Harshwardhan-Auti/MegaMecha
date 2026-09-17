using System.Collections;
using Unity.Android.Gradle.Manifest;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField] private float walkingSpeed = 8f;       //  A normal Speed of player
    [SerializeField] private float sprintingSpeed = 16f;   //  spirnting speed of player
    [SerializeField] private float jumpForce = 14f;       //   player jump force 
    [SerializeField] private float gravityMultiplier = 4f;
    [SerializeField] private float maxFallVelocity = 4.5f;
    [SerializeField] private float staminaBar = 30f;        // too keep value of stamina
    [SerializeField] private float staminaDrainRate = 5f;    // stamina drain 
    [SerializeField] private float staminaRegainRate = 5f;  // stamina regain 
    [SerializeField] private float maxStamina = 15f;        // max stamina player can have
    [SerializeField] private float exhaustionRecoveryThreshold = 5f; // the time till player cant used sprint once he drain all the stamina
    [SerializeField] private float maxPlayerHealth = 100f;
    [SerializeField] private float currentPlayerHealth ;
    

    private float currentSpeed;


    [Header("Player State")]
    [SerializeField] private bool isOnGroud = true;
    [SerializeField] private bool isSprintingKeyHeld = false;
    private bool isActuallySprinting = false;
    private bool isExahuasted = false;
    private bool isMoving;


    private Vector2 movementInput;
    private Vector3 moveDirection;
    private Rigidbody playerRb;


    public float currentStamina => staminaBar;
    public float staminaMax => maxStamina;
    public float playerHealth => currentPlayerHealth;
    public float maxHealth => maxPlayerHealth;






    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.linearVelocity = playerRb.linearVelocity;

    }


    void Update()
    {
        isMoving = movementInput.magnitude > 0.1f;
        HandleStamina();
        isActuallySprinting = isSprintingKeyHeld && staminaBar > 0 && !isExahuasted && isMoving;
        currentSpeed = isActuallySprinting ? sprintingSpeed : walkingSpeed;
        moveDirection = ((movementInput.y * transform.forward) + (movementInput.x * transform.right)); // looks toward forward and right 

        if (isOnGroud)
        {
            playerRb.linearVelocity = new Vector3(moveDirection.x * currentSpeed,
                                        playerRb.linearVelocity.y, moveDirection.z * currentSpeed); // Controlles player movemnets 
        }

        if (currentPlayerHealth <= 0)
        {

            Debug.Log("data khatam Khellll khatammmm !!!!!!!!!");
        
        
        }

    }

    private void FixedUpdate()
    {
        if (playerRb.linearVelocity.y < 0)
        {

            playerRb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);

            if (playerRb.linearVelocity.y < -maxFallVelocity)
            {

                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, -maxFallVelocity, playerRb.linearVelocity.z);

            }

        }
    }



    public void OnMove(InputAction.CallbackContext context)  // to get input value 
    {

        movementInput = context.ReadValue<Vector2>();



    }

    public void OnJump(InputAction.CallbackContext context) // to trigger jump 
    {
        if (context.performed && isOnGroud)
        {
            Jump();

        }




    }

    private void Jump()
    {

        isOnGroud = false;
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0, playerRb.linearVelocity.z);  // for smooth jump , y velo become zero
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);



    }


    public void OnSprinting(InputAction.CallbackContext context)
    {

        isSprintingKeyHeld = context.ReadValueAsButton();



    }


    private void HandleStamina()
    {
        if (staminaBar <= 0)
        {

            isExahuasted = true;

        }
        else if (staminaBar >= exhaustionRecoveryThreshold)
        {

            isExahuasted = false;
        }



        if (isActuallySprinting)
        {
            staminaBar -= staminaDrainRate * Time.deltaTime;


        }
        else if (staminaBar < maxStamina)
        {


            staminaBar += staminaRegainRate * Time.deltaTime;

        }

        staminaBar = Mathf.Clamp(staminaBar, 0f, maxStamina);





    }

    public void GetDamage(float damageByEnemy)
    {
        currentPlayerHealth -= damageByEnemy;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0f, 100f);

    }





    private void OnCollisionEnter(Collision collision)  // to check if player is on ground or not 
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Obstacles"))
        {

            isOnGroud = true;

        }
    }
}
