using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItems(ItemData item, int amount)
    {

        bool foundExistin = false;
        foreach (InventorySlot slot in slots)
        {
            if (item == slot.item)
            {
                foundExistin = true;
                slot.quantity += amount;
                

            }
            
        
        
        }

        if (!foundExistin)
        {

            InventorySlot newSlot = new InventorySlot();
            newSlot.item = item;
            newSlot.quantity = amount;

            slots.Add(newSlot);
            
        
        
        
        
        }
    
    
    }
   
}
