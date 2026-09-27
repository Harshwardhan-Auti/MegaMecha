using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEditor.Progress;


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

    public void SaveInventory()
    {
        InventorySaveData saveData = new InventorySaveData();

        foreach (InventorySlot slot in slots)
        {

            InventorySlotSaveData slotData = new InventorySlotSaveData();
            slotData.itemName = slot.item.itemName;
            slotData.quantity = slot.quantity;
            saveData.slots.Add(slotData);
            
            
        }

        string json = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);


    }
    
    public void LoadInventory()
    {

       string jsonString =  System.IO.File.ReadAllText(Application.persistentDataPath + "/inventory.json" );
        InventorySaveData saveData = JsonUtility.FromJson<InventorySaveData>(jsonString);
        slots.Clear();

       
    }



}

