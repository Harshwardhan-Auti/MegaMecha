using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    private Inventory inventoryScript;

    public void Start()
    {

       
        inventoryScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
       
        inventoryScript.onInventoryChanged += RefreshUI;
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (InventorySlot slot in inventoryScript.inventorySlot)
        {
            GameObject newSlotObj = Instantiate(slotPrefab, transform);
            InventorySlotUi slotUI = newSlotObj.GetComponent<InventorySlotUi>();
            slotUI.SetSlot(slot);
        }


    }
}
