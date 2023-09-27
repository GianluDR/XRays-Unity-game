using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marie : MonoBehaviour
{
    public DialogueTrigger npc;
    private PlayerController player;
    private DialogueManager dialogue;
    private Animator animator;

    void Start(){
        dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        animator = GetComponent<Animator>();
        if(PlayerPrefs.HasKey("LabirintPuzzle")){
            if (PlayerPrefs.GetInt("LabirintPuzzle")>0){
                npc.SetPoint(3);
            }
        }
        if(npc.GetPoint() < 2){
            npc.gameObject.transform.position = new Vector2(0.6720083f,-1.393443f);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        if(npc.GetPoint() > 1){
            npc.gameObject.transform.position = new Vector2(1.58f,0f);
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("test");
        blockTalkTrigger();
        player = hit.gameObject.GetComponent<PlayerController>();
        player.animator.SetBool("IsMoving", false);
        animator.enabled = true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        npc.SetPoint(1);
    }

    private IEnumerator dialogueSerra(){
        UnlockTalkTrigger();
        dialogue.EnterDialogueMode(npc,true);
        yield return StartCoroutine(wait());
        blockTalkTrigger();
        animator.SetTrigger("EndDialogue");
        UnlockTalkTrigger();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void blockTalkTrigger(){
        npc.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void UnlockTalkTrigger(){
        npc.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public IEnumerator wait(){
        while(npc.GetPoint() != 2)
            yield return null;
    }

    public void moveUnlock(){
        player.canMove = true;
    }

    public void blockMove(){
        player.canMove = false;
    }
}
