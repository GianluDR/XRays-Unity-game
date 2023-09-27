using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleTrigger : MonoBehaviour
{
    public PuzzleManager puzzle;
    private PlayerController player;
    public GameObject blockAssociated;
    public Animator animator;
    public List<InventoryItemBase> items;


    public bool puzzleSolved;
    public bool BlockPuzzle;

    private void Start(){
        if(puzzleSolved && animator != null && blockAssociated != null){
            animator.enabled = false;
            blockAssociated.SetActive(false);
        }
    }

    public void startMinigame(){
        if(!BlockPuzzle){
            searchPlayer();
            player.gameObject.SetActive(false);
            puzzle.transform.GetChild(0).gameObject.SetActive(true);
            puzzle.transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    public IEnumerator win(){
        FindObjectOfType<AudioManager>().Play("WinPuzzle");
        puzzle.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(3.6f);
        puzzle.transform.GetChild(0).gameObject.SetActive(false);
        removeItems();
        player.gameObject.SetActive(true);
        puzzleSolved = true;
        if(animator != null)
            animator.SetTrigger("Repaired");
        if(blockAssociated != null)
            blockAssociated.SetActive(false);
    }

    private void removeItems(){
        List<IInventoryItem> mItems = new List<IInventoryItem>();
        for(int i = 0;i<player.inventory.GetSize();i++){
            mItems.Add(player.inventory.GetItemAt(i));
        }
        int count = 0;
        for(int i = 0;i<mItems.Count();i++){
            for(int j = 0;j<items.Count();j++){
                if(mItems[i].Name == items[j].Name){
                    mItems[i] = null;
                    count++;
                    player.inventory.SetItemHov(i-count);
                    Debug.Log(mItems[i]);
                    player.inventory.RemoveItem();
                    j = items.Count();
                }
            }
        }
    }

    public bool checkItems(){
        searchPlayer();
        List<IInventoryItem> mItems = new List<IInventoryItem>();
        for(int i = 0;i<player.inventory.GetSize();i++){
            mItems.Add(player.inventory.GetItemAt(i));
        }
        int count = 0;
        for(int i = 0;i<mItems.Count();i++){
            for(int j = 0;j<items.Count();j++){
                if(mItems[i].Name == items[j].Name){
                    mItems[i] = null;
                    count++;
                    j = items.Count();
                }
            }
        }
        Debug.Log(count);
        Debug.Log(items.Count());
        if(count == items.Count())
            return true;
        else 
            return false;
    }

    public void exitFromPuzzle(){
        puzzle.transform.GetChild(0).gameObject.SetActive(false);
        puzzle.transform.GetChild(1).gameObject.SetActive(false);
        player.gameObject.SetActive(true);
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
