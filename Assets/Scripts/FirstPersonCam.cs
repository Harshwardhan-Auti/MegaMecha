using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCam : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 30f;

    private Vector2 lookInput;
    public Transform playerBody;
    [SerializeField] private Vector3 offset = new Vector3(0f, 0.8f, 0.4f);





    private float yaw;
    private float pitch;

    void Start()
    {

    }


    void LateUpdate()
    {

        yaw += lookInput.x * mouseSensitivity * Time.deltaTime;
        pitch -= lookInput.y * mouseSensitivity * Time.deltaTime;



        pitch = Mathf.Clamp(pitch, -90f, 35f);
        playerBody.localRotation = Quaternion.Euler(0f, yaw, 0f);

        transform.position = playerBody.position + playerBody.rotation * offset;

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);




    }

    public void OnFpLook(InputAction.CallbackContext context)
    {

        lookInput = context.ReadValue<Vector2>();



    }
}
