using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> _inventory;
    
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
