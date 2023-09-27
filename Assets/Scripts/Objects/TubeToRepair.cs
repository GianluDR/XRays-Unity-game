using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TubeToRepair : MonoBehaviour
{
    public PuzzleTrigger trig;
    public Sprite spriteTube;
    private PlayerController player;
    public Sprite questTick;
    public DialogueTrigger DTrig;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DTrig.GetPoint() > 0){
            DTrig.enabled = false;
            DTrig.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            DTrig.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        if(trig.puzzleSolved){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = spriteTube;
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            searchPlayer();
            player.Quest.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = questTick;
        }
    }

    private void searchPlayer(){
        PlayerController mPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i<players.Count() ; i++)
        {
            mPlayer = players[i].GetComponent<PlayerController>();
            if (mPlayer.inParty && mPlayer.used)
            {
                player = mPlayer;
            }
        }
    }
}
