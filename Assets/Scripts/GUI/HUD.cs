using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject InstructionPanel;
    public GameObject InteractionPanel;
    public Inventory Inventory;
    public Sprite itemHov;
    public Sprite itemNotHov;

    void Start()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += Inventory_ItemRemoved;
        Inventory.HoverChange += Inventory_HoverChange;
    }
    
    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        foreach (Transform slot in inventoryPanel)
        {
            Image image = slot.GetChild(0).GetComponent<Image>();
            if(!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;
                break;
            }
        }
    }

    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        int i;
        for(i = 0;i < inventoryPanel.childCount; i++)
        {
            Image image = inventoryPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            if (i == Inventory.slotHov)
            {
                image.enabled = false;
                image.sprite = null;
                break;
            }
        }
        int j = i + 1;
        while(i <= inventoryPanel.childCount-1)
        {
            Image image = inventoryPanel.GetChild(i).GetChild(0).GetComponent<Image>();
            if (i < 3)
            {
                Image imageAfter = inventoryPanel.GetChild(j).GetChild(0).GetComponent<Image>();
                image.enabled = imageAfter.enabled;
                image.sprite = imageAfter.sprite;
            }
            else
            {
                image.enabled = false;
                image.sprite = null;
                break;
            }
            j = j + 1;
            i = i + 1;
        }
    }

    private void Inventory_HoverChange(object sender, InventoryEventArgs e)
    {
        Transform inventoryPanel = transform.Find("Inventory");
        int i = 0;
        foreach (Transform slot in inventoryPanel)
        {
            if (i == Inventory.slotHov)
            {
                Image image = slot.GetComponent<Image>();
                image.sprite = itemHov;
            }
            else
            {
                Image image = slot.GetComponent<Image>();
                image.sprite = itemNotHov;
            }
            i++;
        }
    }

    public void OpenInstructionPanel(string text)
    {
        InstructionPanel.SetActive(true);
    }

    public void CloseInstructionPanel(string text)
    {
        InstructionPanel.SetActive(false);
    }

    public void OpenInteractionPanel(string text)
    {
        InteractionPanel.SetActive(true);
    }

    public void CloseInteractionPanel(string text)
    {
        InteractionPanel.SetActive(false);
    }

}
