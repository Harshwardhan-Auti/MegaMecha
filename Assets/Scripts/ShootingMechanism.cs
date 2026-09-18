using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanism : MonoBehaviour
{
    // [SerializeField] private float fireRate = 0.1f;
    // [SerializeField] private float currentWeapon.bulletRange = 20f;
    // [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private LayerMask hitableLayer;
    [SerializeField] private float nextFireTime= 0f;

   

    [SerializeField] private WeaponData currentWeapon;

    public Transform gunMuzzle;

    public Animator shootAnim;

    [SerializeField] private bool isShooting = false;
    
    private Enemy enemyScript;


    void Start()
    {

        
        
    }

    
    void Update()
    {
        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time * currentWeapon.fireRate;
            
            shootAnim.SetBool("Shooting", true);

        }
        else if (!isShooting)
        {
            shootAnim.SetBool("Shooting", false);
            //shootingAudio.Stop();
        
        }
    }
    public void Shoot()
    {

        Camera cam = Camera.main;
        Debug.Log("shoot() called");
        Vector3 origin = gunMuzzle.position;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, currentWeapon.bulletRange, hitableLayer))
        {
            Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
            
            if (hitEnemy != null)
            {
                hitEnemy.TakeDamage(currentWeapon.bulletDamage);
                Debug.Log("Shots Fireeee!!!(Dramatically)");
            }
           


        }


    }


    private void OnDrawGizmos()
    {

        if (Camera.main == null || gunMuzzle == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunMuzzle.position, gunMuzzle.position + Camera.main.transform.forward * currentWeapon.bulletRange);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           
            isShooting = true;

        }

        if (context.canceled)
        {
            isShooting = false;

        }


    }
}
