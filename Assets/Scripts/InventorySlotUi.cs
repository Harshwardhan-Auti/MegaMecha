using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class InventorySlotUi : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;


    public void SetSlot(InventorySlot slot)
    {
        Debug.Log("Setting slot, icon is: " + slot.item.itemIcon);
        iconImage.sprite = slot.item.itemIcon;
        quantityText.text = slot.quantity.ToString();

    
    
    }
    
    
}
