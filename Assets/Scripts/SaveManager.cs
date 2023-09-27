using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : ScriptableObject
{
    private PuzzleTrigger puzzle;
    private int puzzleSolved;

    private List<GameObject> NPCs;
    private List<GameObject> Walls;

    public void SaveData(){
        string id = null;
        try { 
            puzzle = GameObject.FindWithTag("Puzzle").GetComponent<PuzzleTrigger>();
        }catch { /*NullReferenceException here*/ }
        if(puzzle != null){
            id = SceneManager.GetActiveScene().name+"Puzzle";
            bool solved = puzzle.puzzleSolved;
            if(solved)
                puzzleSolved = 1;
            else    
                puzzleSolved = 0;
            PlayerPrefs.SetInt(id,puzzleSolved);
        }
        try {
            NPCs = GameObject.FindGameObjectsWithTag("NPC").ToList();
        }catch { /*NullReferenceException here*/ }
        if(NPCs != null){
            for(int i = 0; i < NPCs.Count(); i++){
                DialogueTrigger NPC = NPCs[i].GetComponent<DialogueTrigger>();
                Debug.Log(NPC.gameObject.transform.parent);
                id = SceneManager.GetActiveScene().name+"NpcPoint"+i;
                PlayerPrefs.SetInt(id,NPC.GetPoint());
                id = SceneManager.GetActiveScene().name+"NpcX"+i;
                PlayerPrefs.SetFloat(id,NPCs[i].transform.position.x);
                id = SceneManager.GetActiveScene().name+"NpcY"+i;
                PlayerPrefs.SetFloat(id,NPCs[i].transform.position.y);
            }
        }
        try { 
            Walls = GameObject.FindGameObjectsWithTag("Wall").ToList();
        }catch { /*NullReferenceException here*/ }
         if(Walls != null){
            for(int i = 0; i < Walls.Count(); i++){
                WallToRepair wall = Walls[i].GetComponent<WallToRepair>();
                id = SceneManager.GetActiveScene().name+"Wall"+i;
                if(wall.repaired)
                    PlayerPrefs.SetInt(id,1);
                else
                    PlayerPrefs.SetInt(id,0);
            }
        }
    }

    public void RestoreData(){
        string id = null;
        id = SceneManager.GetActiveScene().name+"Puzzle";
        if (PlayerPrefs.HasKey(id)){
            puzzle = GameObject.FindWithTag("Puzzle").GetComponent<PuzzleTrigger>();
            if(puzzle != null){
                int solved = PlayerPrefs.GetInt(id);
                if(solved == 1){
                    GameObject.FindWithTag("Puzzle").GetComponent<PuzzleTrigger>().puzzleSolved = true;
                }
                
            }
        }
        NPCs = GameObject.FindGameObjectsWithTag("NPC").ToList();
        if(NPCs != null){
            for(int i = 0; i < NPCs.Count(); i++){
                DialogueTrigger NPC = NPCs[i].GetComponent<DialogueTrigger>();
                id = SceneManager.GetActiveScene().name+"NpcPoint"+i;
                if (PlayerPrefs.HasKey(id)){
                    NPC.SetPoint(PlayerPrefs.GetInt(id));
                    id = SceneManager.GetActiveScene().name+"NpcX"+i;
                    float x = PlayerPrefs.GetFloat(id);
                    id = SceneManager.GetActiveScene().name+"NpcY"+i;
                    float y = PlayerPrefs.GetFloat(id);
                    NPCs[i].transform.parent.gameObject.transform.position = new Vector3(x,y,0);
                }
            }
        }
        Walls = GameObject.FindGameObjectsWithTag("Wall").ToList();
        if(Walls != null){
            for(int i = 0; i < Walls.Count(); i++){
                WallToRepair wall = Walls[i].GetComponent<WallToRepair>();
                id = SceneManager.GetActiveScene().name+"Wall"+i;
                if (PlayerPrefs.HasKey(id)){
                    int repaired = PlayerPrefs.GetInt(id);
                    if(repaired == 1){
                        wall.repaired = true;
                    }
                }
            }
        }
    }
}
