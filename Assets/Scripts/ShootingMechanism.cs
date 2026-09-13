using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanism : MonoBehaviour
{
    [SerializeField] private float bulletRange = 20f;
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private LayerMask hitableLayer;

    public Transform gunMuzzle;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    public void Shoot()
    {

        Camera cam = Camera.main;
        Vector3 origin = gunMuzzle.position;
        Vector3 direction = cam.transform.forward;
        
        if (Physics.Raycast(origin, direction, bulletRange, hitableLayer))
        {
            Debug.Log("Shots Fireeee!!!(Dramatically)");
        
        }
    
    
    }


    private void OnDrawGizmos()
    {

        if (Camera.main == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunMuzzle.position, gunMuzzle.position + Camera.main.transform.forward * bulletRange);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();

        }
    
    
    }
}
