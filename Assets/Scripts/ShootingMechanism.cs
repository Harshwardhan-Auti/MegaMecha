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
    [SerializeField] private GameObject currentWeaponModel;

    public Transform gunMuzzle;

    private Animator shootAnim;

    [SerializeField] private bool isShooting = false;
    
    private Enemy enemyScript;


    void Start()
    {

        
        
    }

    
    void Update()
    {
        if (currentWeapon == null) return;
        if (currentWeaponModel== null) return;

        

        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + currentWeapon.fireRate;
            
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
        if (currentWeapon == null) return;
        if (currentWeaponModel == null) return;

        Camera cam = Camera.main;
       
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
        if (currentWeapon == null) return;
        if (currentWeaponModel == null) return;

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


    public void EquipWeapon(WeaponData newWeapon)
    {
        if (currentWeaponModel != null)
        {
            Destroy(currentWeaponModel);
        }

        currentWeapon = newWeapon;
        currentWeaponModel = Instantiate(newWeapon.prefab, gunMuzzle);
        shootAnim = currentWeaponModel.GetComponent<Animator>();
    }



}
