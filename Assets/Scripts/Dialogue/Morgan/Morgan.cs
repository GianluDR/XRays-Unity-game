using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morgan : MonoBehaviour
{
    private PlayerController player;
    private DialogueManager dialogue;
    public DialogueTrigger npc;
    private Animator animator;
    public PuzzleTrigger trig;
    // Start is called before the first frame update
    void Start()
    {
        searchPlayer();
        blockTalkTrigger();
        animator = GetComponent<Animator>();
        if(npc.GetPoint() > 0 && npc.GetPoint() != 2){
            animator.enabled = false;
        }
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(trig.puzzleSolved)
            npc.SetPoint(4);
    }

    public IEnumerator TutorialMov(){
        GameObject tutorialMov = GameObject.Find("Tutorial").transform.GetChild(0).gameObject;
        tutorialMov.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialMov.SetActive(false);
    }

    public IEnumerator SecondDialogue(){
        npc.SetPoint(2);
        dialogue.EnterDialogueMode(npc,true);
        yield return StartCoroutine(waitTwo());
        animator.SetTrigger("WaitInLake");
        trig.BlockPuzzle = false;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        moveUnlock();
    }

    public IEnumerator waitTwo(){
        while(npc.GetPoint() != 3)
            yield return null;
    }

    public IEnumerator firstDialogue(){       
        dialogue.EnterDialogueMode(npc,true);
        yield return StartCoroutine(wait());
        animator.SetTrigger("moveOut");
    }

    public IEnumerator wait(){
        while(npc.GetPoint() != 1)
            yield return null;
    }

    public void blockTalkTrigger(){
        npc.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void UnlockTalkTrigger(){
        npc.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void moveUnlock(){
        player.canMove = true;
    }

    public void blockMove(){
        player.canMove = false;
    }

    public void destroy(){
        this.gameObject.SetActive(false);
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