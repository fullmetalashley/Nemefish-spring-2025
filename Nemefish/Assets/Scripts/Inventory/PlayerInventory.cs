using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> _inventory;

    public int playerHealth = 500;

    public void AddItem(Item toAdd)
    {
        if (CheckForItem(toAdd))
        {
            //Don't add it, get the index and add to the quantity.
            _inventory[ItemIndex(toAdd)]._quantity++;
        }
        else
        {
            _inventory.Add(toAdd);
        }
        // Play SFX for adding item to inventory
        AudioManager.instance.PlayOneShot(FMODEvents.instance.addItemToInventory, this.transform.position);
    }

    public int ReturnItemCount(string itemName)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i]._itemName == itemName)
            {
                return _inventory[i]._quantity;
            }
        }
        return 0;
    }
    
    

    //Does this item exist in the inventory?
    //Not entirely needed to do it this way, but it's a safety measure. 
    private bool CheckForItem(Item itemToAdd)
    {
        foreach (var t in _inventory)
        {
            if (t._itemName == itemToAdd._itemName)
            {
                return true;
            }
        }
        return false;
    }

    //Where is this item in the inventory?
    private int ItemIndex(Item itemToAdd)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i]._itemName == itemToAdd._itemName)
            {
                return i;
            }
        }
        return -1;
    }
}
