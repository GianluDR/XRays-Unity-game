using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private const int SLOTS = 4;
    private List<IInventoryItem> mItems = new List<IInventoryItem>();
    
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> HoverChange;

    public int slotHov = 0;

    public int GetSize(){ return mItems.Count; }

    public void SetItemHov(int i)
    {
        if(!(i < 0 || i >= SLOTS))
            slotHov = i;
        if (HoverChange != null)
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public IInventoryItem GetItemHov()
    {
        if (mItems.Count == 0 || (slotHov > mItems.Count - 1))
        {
            return null;
        }
        else
        {
            return mItems[slotHov];
        }   
    }

    public IInventoryItem GetItemAt(int i)
    {
        if (mItems.Count == 0)
        {
            return null;
        }
        else
        {
            return mItems[i];
        }   
    }

    public void AddItem(IInventoryItem item)
    {
        if(mItems.Count < SLOTS)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if(collider.enabled)
            {
                collider.enabled = false;
                mItems.Add(item);
                item.OnPickup();

                if(ItemAdded != null)
                {
                    ItemAdded(this,new InventoryEventArgs(item));
                }
            }
        }
    }

    public void RemoveItem()
    {
        IInventoryItem item = GetItemHov();
        if (mItems.Contains(item))
        {
            mItems.Remove(item);

            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if(collider != null)
            {
                collider.enabled = true;
            }

            if(ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }

    public void DropItem()
    {
        IInventoryItem item = GetItemHov();
        if (mItems.Contains(item))
        {
            mItems.Remove(item);
            item.OnDrop();

            /*Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if(collider != null)
            {
                collider.enabled = true;
            }*/

            if(ItemRemoved != null)
            {
                ItemRemoved(this, new InventoryEventArgs(item));
            }
        }
    }

    public void GoHoverRight()
    {
        slotHov = slotHov + 1;
        if (slotHov > 3)
            slotHov = 0;
        
        if(HoverChange != null) 
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public void GoHoverLeft()
    {
        slotHov = slotHov - 1;
        if (slotHov < 0)
            slotHov = 3;

        if (HoverChange != null)
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public void ToSlot0()
    {
        slotHov = 0;
        
        if(HoverChange != null) 
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public void ToSlot1()
    {
        slotHov = 1;
        
        if(HoverChange != null) 
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public void ToSlot2()
    {
        slotHov = 2;
        
        if(HoverChange != null) 
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }

    public void ToSlot3()
    {
        slotHov = 3;
        
        if(HoverChange != null) 
        {
            HoverChange(this, new InventoryEventArgs());
        }
    }
}
