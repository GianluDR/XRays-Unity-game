using System;
using System.Collections;
using System.Windows;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneShip : MonoBehaviour
{
    public DialogueManager dialogue;
    public DialogueTrigger npcToTalk;
    public PlayableDirector CutScene;
    public GameObject Crossfade;
    public Animator animator,transition;
    public PauseMenuScript pause;
    private bool dialogueStarted = false;

    void start(){
        Crossfade.SetActive(false);
    }

    void Update(){
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("StarLoop") && (!dialogueStarted))
        {
            StartCoroutine(PlayTimelineRoutine());
            dialogueStarted = true;
        }
        if(npcToTalk.GetPoint()>0){
            StartCoroutine(EarthArrive());
        }
    }

    private void OnInteraction()
    {
        dialogue.ContinueStory();
    }

    private void OnPause()
    {
        if (pause.GameIsPaused)
            pause.Resume();
        else
            pause.Pause(); 
    }

    private void OnSpeed()
    {
        if(Time.timeScale>1)
            Time.timeScale = 1f;
        else
            Time.timeScale = 35f;
    }

    private IEnumerator EarthArrive(){
            animator.SetTrigger("DialogueEnd");
            Crossfade.SetActive(true);
            transition.SetTrigger("start");
            yield return new WaitForSeconds(1.5f);
            SceneManager.LoadScene("Home");
    }

    private IEnumerator PlayTimelineRoutine()
    {
        CutScene.Play();
        yield return new WaitForSeconds((float) CutScene.duration);
        yield return null;
        dialogue.EnterDialogueMode(npcToTalk,false);
    }
}
