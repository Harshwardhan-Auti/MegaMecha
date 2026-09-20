using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;



public class InventorySlotUi : MonoBehaviour,IPointerClickHandler, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private InventorySlot currentSlot;

    private GameObject dragIcon;

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

    public void OnBeginDrag(PointerEventData eventData)
    {

        dragIcon = new GameObject("DragIcon");
        dragIcon.transform.SetParent(transform.root);
        Image img = dragIcon.AddComponent<Image>();
        img.sprite = currentSlot.item.itemIcon;
        img.raycastTarget = false; // so it doesnt block cursor from detecting whats under

    
    
    
    }

    public void OnDrag(PointerEventData eventData)
    {

        dragIcon.transform.position = eventData.position;
    
    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(dragIcon);

        GameObject targetGameobj = eventData.pointerCurrentRaycast.gameObject;

        if (targetGameobj == null) return;

        InventorySlotUi targetSlot = targetGameobj.GetComponent<InventorySlotUi>();
        if (targetSlot == null) return;

        InventorySlot temp = this.currentSlot;
        this.currentSlot = targetSlot.currentSlot;
        targetSlot.currentSlot = temp;

        this.SetSlot(this.currentSlot);
        targetSlot.SetSlot(targetSlot.currentSlot);
    
    
    }

    public void SetSlot(InventorySlot slot)
    {
        currentSlot = slot;
        iconImage.sprite = slot.item.itemIcon;
        quantityText.text = slot.quantity.ToString();



    
    
    }

    


    
    
}
