using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingMechanism : MonoBehaviour
{
    [SerializeField] private float bulletRange = 20f;
    [SerializeField] private float bulletDamage = 10f;
    [SerializeField] private LayerMask hitableLayer;
    [SerializeField] private float fireRate = 0.1f;

    public Transform gunMuzzle;

    public Animator shootAnim;

    [SerializeField] private bool isShooting = false;

    private Enemy enemyScript;


    void Start()
    {

        
        
    }

    
    void Update()
    {
        if (isShooting && Time.time >= fireRate)
        {
            Shoot();
            
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
        Vector3 origin = gunMuzzle.position;
        Vector3 direction = cam.transform.forward;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, bulletRange, hitableLayer))
        {
            Enemy hitEnemy = hit.collider.GetComponent<Enemy>();
            Debug.Log("Shots Fireeee!!!(Dramatically)");
            hitEnemy.TakeDamage(bulletDamage);


        }


    }


    private void OnDrawGizmos()
    {

        if (Camera.main == null || gunMuzzle == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(gunMuzzle.position, gunMuzzle.position + Camera.main.transform.forward * bulletRange);
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
