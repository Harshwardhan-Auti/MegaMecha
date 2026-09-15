using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 30f;

    private Vector2 lookInput;
    public Transform playerBody;

    //[SerializeField] private Vector3 tpOffset = new Vector3(0, 1.5f, -4);
    //[SerializeField] private Vector3 fpOffset = new Vector3(0f, 0.8f, 0.4f);
    [SerializeField] private Vector3 offset;



    private float yaw;
    private float pitch;


    void Start()
    {

    }


    void LateUpdate()
    {

        // Vector3 offset = isFirstPerson ? fpOffset : tpOffset; // check weather current camera is tp or fp 

        yaw += lookInput.x * mouseSensitivity * Time.deltaTime;
        pitch -= lookInput.y * mouseSensitivity * Time.deltaTime;



        pitch = Mathf.Clamp(pitch, -10f, 35f);
        playerBody.localRotation = Quaternion.Euler(0f, yaw, 0f);

        transform.position = playerBody.position + playerBody.rotation * offset;

        // transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);




    }

    public void OnLook(InputAction.CallbackContext context)
    {

        lookInput = context.ReadValue<Vector2>();



    }
}
