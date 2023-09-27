using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCut : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hit)
    {
        Sword sword = hit.gameObject.GetComponent<Sword>();
        if(sword!=null){
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);
        }
    }

}
