using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eliza : MonoBehaviour
{
    public WallToRepair[] Walls;
    public DialogueTrigger npc;
    private PlayerController player;
    public Sprite questTick;
    
    void Update(){
        if(checkWalls()){
            npc.SetPoint(1);
            searchPlayer();
            player.Quest.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = questTick;
        }
    }

    private bool checkWalls(){
        int count = 0;
        for(int i = 0; i<Walls.Count();i++){
            if(Walls[i].repaired)
                count++;
        }
        if(Walls.Count() == count)
            return true;
        else 
            return false;
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