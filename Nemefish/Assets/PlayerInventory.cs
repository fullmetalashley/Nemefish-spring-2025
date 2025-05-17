using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> _inventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(Fish fishToAdd)
    {
        Item toAdd = fishToAdd._successItem;
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
