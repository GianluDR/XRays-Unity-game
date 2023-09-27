using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharacterSwitch : MonoBehaviour
{
    public List<GameObject> players;
    private List<GameObject> playersLeft;
    private int i;
    private Button btnUL;
    private int swapLeft;
    private bool left;

    void Start()//OnEnable()
    {
        left = false;
        playersLeft = new List<GameObject>();
        btnUL = GameObject.Find("ButtonPgUL").GetComponent<Button>();
        btnUL.Select();
        btnUL.OnSelect(null);
        i = 0;
        GameObject.Find("ArrowUp").GetComponent<Image>().enabled = false;
        searchPlayer();
    } 

    public void menuSwitchLR(int id){
        if(players[id] != null){
            Button btnSwitch1 = GameObject.Find("ButtonSwitch1").GetComponent<Button>();
            btnSwitch1.Select();
            btnSwitch1.OnSelect(null);
            swapLeft = id;
            left = true;
        }
    }

    public void menuSwitchRL(int id){
        if(playersLeft[swapLeft] != players[i+id]){
            btnUL = GameObject.Find("ButtonPgUL").GetComponent<Button>();
            btnUL.Select();
            btnUL.OnSelect(null);
            playersLeft[swapLeft] = players[i+id];
            Debug.Log(swapLeft);
            Debug.Log(playersLeft[swapLeft]);
            GameObject player = GameObject.Find("Face ("+(swapLeft+1)+")");
            Image face = player.GetComponent<Image>();
            face.sprite = playersLeft[swapLeft].GetComponent<PlayerController>().face;
            left = false;
        }else{
            Debug.Log("NON PUOI SCAMBIARE DUE UGUALI.");
        }
    }

    public void backToL(){
        if(left){
            btnUL = GameObject.Find("ButtonPgUL").GetComponent<Button>();
            btnUL.Select();
            btnUL.OnSelect(null);
        }else{
            this.gameObject.SetActive(false);
        }

    }

    public void goUp(){
        if(i>0){
            if(EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSwitch1")){
                i = i-1;
                if(i == 0){
                    GameObject arrowUp = GameObject.Find("ArrowUp");
                    arrowUp.GetComponent<Image>().enabled = false;
                }
                GameObject arrowDown = GameObject.Find("ArrowDown");
                arrowDown.GetComponent<Image>().enabled = true;
                GameObject playerR1 = GameObject.Find("Pg (1)");
                Image faceR1 = playerR1.GetComponent<Image>();
                faceR1.sprite = players[i].GetComponent<PlayerController>().face;
                GameObject playerR2 = GameObject.Find("Pg (2)");
                Image faceR2 = playerR2.GetComponent<Image>();
                faceR2.sprite = players[i+1].GetComponent<PlayerController>().face;
                GameObject playerR3 = GameObject.Find("Pg (3)");
                Image faceR3 = playerR3.GetComponent<Image>();
                faceR3.sprite = players[i+2].GetComponent<PlayerController>().face;
            }
        }
    }

    public void OnNavigate(InputAction.CallbackContext ctx)
    {
        if(ctx.performed){
            if(EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSwitch1")
                || EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSwitch2")
                || EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSwitch3")){
                if(ctx.ReadValue<Vector2>() == new Vector2(0, 1))
                    goUp();
                else if(ctx.ReadValue<Vector2>() == new Vector2(0, -1))
                    goDown();
            }
        }
    }

    public void goDown(){
        if(i+2<players.Count()-1){
            if(EventSystem.current.currentSelectedGameObject == GameObject.Find("ButtonSwitch3")){
                i = i+1;
                if(i+2 == players.Count()-1){
                    GameObject arrowDown = GameObject.Find("ArrowDown");
                    arrowDown.GetComponent<Image>().enabled = false;
                }
                GameObject arrowUp = GameObject.Find("ArrowUp");
                arrowUp.GetComponent<Image>().enabled = true;
                GameObject playerR1 = GameObject.Find("Pg (1)");
                Image faceR1 = playerR1.GetComponent<Image>();
                faceR1.sprite = players[i].GetComponent<PlayerController>().face;
                GameObject playerR2 = GameObject.Find("Pg (2)");
                Image faceR2 = playerR2.GetComponent<Image>();
                faceR2.sprite = players[i+1].GetComponent<PlayerController>().face;
                GameObject playerR3 = GameObject.Find("Pg (3)");
                Image faceR3 = playerR3.GetComponent<Image>();
                faceR3.sprite = players[i+2].GetComponent<PlayerController>().face;
            }
        }
    }

    private void searchPlayer(){
        PlayerController mPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerController player = null;
        for (int j = 0; j<players.Count() ; j++)
        {
            mPlayer = players[i].GetComponent<PlayerController>();
            if (mPlayer.inParty || mPlayer.used)
            {
                player = mPlayer;
            }
        }
        Debug.Log(player);
        setPlayersList(player.players);
    }

    private void setPlayersList(List<GameObject> playersInUse){
        for(int j = 0;j<playersInUse.Count();j++){
            if(playersInUse[j] != null){
                GameObject player = GameObject.Find("Face ("+(j+1)+")");
                Image face = player.GetComponent<Image>();
                face.sprite = playersInUse[j].GetComponent<PlayerController>().face;
                playersLeft.Add(playersInUse[j]);
            }
        }
        GameObject playerR1 = GameObject.Find("Pg (1)");
        Image faceR1 = playerR1.GetComponent<Image>();
        faceR1.sprite = players[0].GetComponent<PlayerController>().face;
        GameObject playerR2 = GameObject.Find("Pg (2)");
        Image faceR2 = playerR2.GetComponent<Image>();
        faceR2.sprite = players[1].GetComponent<PlayerController>().face;
        GameObject playerR3 = GameObject.Find("Pg (3)");
        Image faceR3 = playerR3.GetComponent<Image>();
        faceR3.sprite = players[2].GetComponent<PlayerController>().face;
    }

}
