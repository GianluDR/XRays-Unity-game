using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        PlayerController player = hit.gameObject.GetComponent<PlayerController>();
        Debug.Log(player);
        if(player != null){
            this.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }

    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        PlayerController player = hit.gameObject.GetComponent<PlayerController>();
        if(player != null){
            this.gameObject.transform.parent.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
