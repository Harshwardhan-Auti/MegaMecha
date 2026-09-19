using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;



public class InventorySlotUi : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private InventorySlot currentSlot;

    private ShootingMechanism shootingMechanismScript;

    public void Start()
    {

        shootingMechanismScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingMechanism>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        WeaponData weapon = currentSlot.item as WeaponData;
        if (weapon != null)
        {

            shootingMechanismScript.EquipWeapon(weapon);
        
        }
        
        
    }

    public void SetSlot(InventorySlot slot)
    {
        currentSlot = slot;
        iconImage.sprite = slot.item.itemIcon;
        quantityText.text = slot.quantity.ToString();



    
    
    }

    


    
    
}
