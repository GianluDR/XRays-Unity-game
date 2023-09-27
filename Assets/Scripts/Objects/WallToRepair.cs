using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallToRepair : MonoBehaviour
{
    public bool repaired;
    public InventoryItemBase wood;

    public void Repair(PlayerController player){
        if(toggleWood(player)){
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            repaired = true;
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

    private bool toggleWood(PlayerController player){
        bool removed = false;
        List<IInventoryItem> mItems = new List<IInventoryItem>();
        for(int i = 0;i<player.inventory.GetSize();i++){
            mItems.Add(player.inventory.GetItemAt(i));
        }
        for(int i = 0;i<mItems.Count();i++){
            if(mItems[i].Name == wood.Name){
                mItems[i] = null;
                player.inventory.SetItemHov(i);
                player.inventory.RemoveItem();
                removed = true;
                i = mItems.Count();
            }
        }
        return removed;
    }
}
