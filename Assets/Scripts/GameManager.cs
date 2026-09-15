using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Camera fpCam;
    public Camera tpCam;
    private bool isFirstPerson = false;

    private PlayerController playerControllerScript;
    public UnityEngine.UI.Image actualStaminaImage;
    public UnityEngine.UI.Image playerHealthImage;




    void Start()
    {
        LockCursor();
        fpCam.enabled = false;
        tpCam.enabled = true;

        fpCam.GetComponent<AudioListener>().enabled = false;
        tpCam.GetComponent<AudioListener>().enabled = true;

        playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }


    void Update()
    {
        actualStaminaImage.fillAmount = playerControllerScript.currentStamina / playerControllerScript.staminaMax;

        playerHealthImage.fillAmount = playerControllerScript.playerHealth / playerControllerScript.maxHealth;
    }


    private void LockCursor()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

    }

    private void UnlockCursor()
    {

        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

    }

    public void OnCameraSwitch(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            isFirstPerson = !isFirstPerson;
            fpCam.enabled = isFirstPerson;
            tpCam.enabled = !isFirstPerson;

            fpCam.GetComponent<AudioListener>().enabled = isFirstPerson;
            tpCam.GetComponent<AudioListener>().enabled = !isFirstPerson;



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
