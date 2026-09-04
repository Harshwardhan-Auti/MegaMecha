using Unity.Android.Gradle.Manifest;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [Header("Player Values")]
    [SerializeField] private float walkingSpeed = 8f;       //  A normal Speed of player
    [SerializeField] private float sprintingSpeed = 16f;   //  spirnting speed of player
    [SerializeField] private float jumpForce = 14f;       //   player jump force 
    //[SerializeField]private float gravityMultiplier = 4f;
    private float currentSpeed;


    [Header("Player State")]
    [SerializeField] private bool isOnGroud = true;
    [SerializeField] private bool isSprinting = false;


    private Vector2 movementInput;
    private Rigidbody playerRb;
    




    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.linearVelocity = playerRb.linearVelocity;
        
    }


    void Update()
    {

        currentSpeed = isSprinting ? sprintingSpeed : walkingSpeed;
        playerRb.linearVelocity = new Vector3(movementInput.x * currentSpeed, 
                                    playerRb.linearVelocity.y, movementInput.y * currentSpeed); // Controlles player movemnets 

        
    }

    //private void FixedUpdate()
    //{
    //    if (playerRb.linearVelocity.y < 0)
    //    {

    //        playerRb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration);
        
    //    }
    //}



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

        isSprinting = context.ReadValueAsButton();
    
    
    }





    private void OnCollisionEnter(Collision collision)  // to check if player is on ground or not 
    {
        if (collision.gameObject.CompareTag("Platform"))
        {

            isOnGroud = true;
        
        }
    }
}
