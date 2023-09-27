using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public GameObject prefab;

    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
        gameObject.transform.SetParent(GameObject.FindGameObjectsWithTag("Player")[0].transform); 
        gameObject.transform.localScale = new Vector3(1,1,1);
    }

    public virtual void OnDrop()
    {
        GameObject prefabItem = Instantiate(
            prefab
        );
        prefabItem.name = Name;
        prefabItem.transform.position = GameObject.FindGameObjectsWithTag("Player")[0].transform.position;
        prefabItem.SetActive(true);
        prefabItem.GetComponent<BoxCollider2D>().enabled = true;
    }
}
