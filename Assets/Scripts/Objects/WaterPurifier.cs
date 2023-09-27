using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterPurifier : MonoBehaviour
{
    int water;
    private int stack;
    public List<SpriteRenderer> stacks;
    float timeLeft = 5.0f;
    private bool morgan;
    private PlayerController player;
    Animator anim;
    public Sprite questTick;

    public bool repaired;

    void Start(){
        water = 25;
        stack = 2;
        morgan = false;
        anim = GetComponent<Animator>();
    }

    void Update(){
        if(this.gameObject.GetComponent<PuzzleTrigger>().puzzleSolved){
            searchPlayer();
            player.Quest.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = questTick;
        }
        if(stack < 2){
            timeLeft -= Time.deltaTime;
            if ( timeLeft < 0 )
            {
                stack++;
                stacks[stack].enabled = false;
                timeLeft = 5.0f;
            }
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

    public int getWater(){
        checkStatus();
        if(repaired){
            if(stack >= 0){
                stacks[stack].enabled = true;
                stack--;
                return water;
            }else
                return 0;
        }else{
            if(!morgan && !repaired)
                moveMorgan();
            return 0;
        }
    }

    public void checkStatus(){repaired = this.gameObject.GetComponent<PuzzleTrigger>().puzzleSolved;}

    private void moveMorgan(){
        GameObject morganObj = GameObject.Find("NPC_John");
        morganObj.GetComponent<SpriteRenderer>().enabled = true;
        Animator morganAnim = morganObj.GetComponent<Animator>();
        morganAnim.enabled = true;
        morganAnim.SetBool("toLake",true);
        morgan = true;
    }
}
