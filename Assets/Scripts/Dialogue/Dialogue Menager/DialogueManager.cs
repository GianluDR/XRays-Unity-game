using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.01f;

    [Header("Dialogue UI")]
    [SerializeField] public GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject pressF;
    [SerializeField] private Animator transition;

    [Header("FaceOfWhoTalk")]
    [SerializeField] private GameObject face; 
    
    private PlayerController player;
    public Sprite playerFace;
    private DialogueTrigger npcToTalk;
    
    private Story currentStory;

    private string tagCharacter;
    private string tagCharacterProfile;

    private Coroutine displayLineCoroutine; 
    private bool lockMove;
    private bool dialogueSound=false;
    private void Start()
    {
        dialoguePanel.SetActive(false);
    }
    
    public void EnterDialogueMode(DialogueTrigger npc,bool flag)
    {   
        dialogueSound=true;
        searchPlayer();
        try{
            player.canMove = false;
            player.animator.SetBool("IsMoving", false);
        }catch{ }
        lockMove = flag;
        npc.gameObject.GetComponent<SpriteRenderer>().enabled = false;

        npcToTalk = npc;
        TextAsset inkJSON = npc.inkJSON;
        currentStory = new Story(inkJSON.text);
        dialoguePanel.SetActive(true);
        currentStory.variablesState["variabile"] = npcToTalk.GetPoint();
        ContinueStory();
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

    public void ExitDialogueMode()
    {
        npcToTalk.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
        try{
            if(!lockMove)
                player.canMove = true;
        }catch{ }
        dialogueSound=false;
    }
    
    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {   
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));

            List<string> tags = currentStory.currentTags;
            if (tags.Count > 0)
            {
                if (tags[0] != "Player") 
                {
                    face.GetComponent<Image>().sprite = npcToTalk.npcFace;
                }
                else
                {
                    face.GetComponent<Image>().sprite = playerFace;
                }
            }
        }
        else
        {
            if(npcToTalk.crossfade){
                transition = GameObject.Find("Crossfade Animator").GetComponent<Animator>();
                transition.speed = 1f;
                transition.SetTrigger("start");
                transition.SetTrigger("end");
            }
            ExitDialogueMode(); 
            npcToTalk.SetPointUp();
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        pressF.SetActive(false);
        dialogueText.text = " ";
        foreach (char letter in line.ToCharArray()) 
        {
            dialogueText.text += letter;
            try{
                if(dialogueSound)
                FindObjectOfType<AudioManager>().waitPlaying("dialogue");
            }catch{}
            yield return new WaitForSeconds(typingSpeed);
        }
        pressF.SetActive(true);
    }

}
