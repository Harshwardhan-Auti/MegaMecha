using UnityEngine;

public class PickupItems : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private int amount = 1;
 
    
   


    private void OnTriggerEnter(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if (inventory != null)
        {

            inventory.AddItems(item, amount);
            Destroy(gameObject);
            
            
        }
    }


}
