using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject fpCam;
    public GameObject tpCam;
    private bool isFirstPerson = false;

    private PlayerController playerControllerScript;
    public UnityEngine.UI.Image actualStaminaImage;

    
    void Start()
    {
        LockCursor();
        fpCam.SetActive(false);
        tpCam.SetActive(true);

        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        actualStaminaImage.fillAmount = playerControllerScript.currentStamina / playerControllerScript.staminaMax;
    }


    private void LockCursor()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
    
    }

    private void UnlockCursor()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true ;

    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            isFirstPerson = !isFirstPerson;
            fpCam.SetActive(isFirstPerson);
            tpCam.SetActive(!isFirstPerson);

        }

            
    
    
    }

    public void OnUnlocking(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UnlockCursor();
        
        }
    
    }

}
