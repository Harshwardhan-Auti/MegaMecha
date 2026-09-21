using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices.WindowsRuntime;


public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> slots = new List<InventorySlot>();

    [SerializeField] private List<ItemData> allPossibleItems;

    public List<InventorySlot> inventorySlot => slots;

    public event Action onInventoryChanged;

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

        onInventoryChanged.Invoke();
        
    
    }


    public ItemData FindItemByName(string name)
    {

        foreach (ItemData item in allPossibleItems)
        {

            if (item.itemName == name)
            {

                return item;
                
            }
            
            
        }
        return null;
    
    }
   
}
