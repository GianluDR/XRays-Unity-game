using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rad : MonoBehaviour
{
    public float radMultiplier;

    private void OnTriggerStay2D(Collider2D hit)
    {
        try{
        PlayerManager player = hit.gameObject.GetComponent<PlayerManager>();
        player.radEffect(radMultiplier);
        }catch{}
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        try{
            PlayerManager player = hit.gameObject.GetComponent<PlayerManager>();
            player.stopRad();
        }catch{}
    }
}
