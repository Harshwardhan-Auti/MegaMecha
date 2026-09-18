using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")] 
public class WeaponData : ItemData
{
   

    
    public float fireRate;
    public float bulletDamage;
    public int magzineSize;
    public float bulletRange;

}
