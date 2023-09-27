using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frontier : MonoBehaviour
{
    public DialogueTrigger Thinks;

    public IEnumerator Start(){
        GameObject tutorialAtt = GameObject.Find("Tutorial").transform.GetChild(1).gameObject;
        Debug.Log(tutorialAtt);
        tutorialAtt.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialAtt.SetActive(false);
    }

    private IEnumerator OnTriggerEnter2D(Collider2D hit)
    {
        DialogueManager dialogue = null;
        HUD hud = null;
        try{
            dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();  
            hud = GameObject.Find("GUI").GetComponent<HUD>();  
            dialogue.EnterDialogueMode(Thinks,false);
        }catch{}
        hud.CloseInteractionPanel("");
        yield return new WaitForSeconds(3f);
        dialogue.ExitDialogueMode();
        this.gameObject.transform.position = new Vector2(100,100);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    
    private void OnTriggerStay2D(Collider2D hit){
        HUD hud = null;
        hud = GameObject.Find("GUI").GetComponent<HUD>();  
        hud.CloseInteractionPanel("");
    }
}
