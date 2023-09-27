using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PgStatsScript : MonoBehaviour
{

    private PlayerController player;
    private List<GameObject> party;
    private GameObject[] bars;
    private int Nparty;
    public GameObject pg1;
    public GameObject pg2;
    public GameObject pg3;

    void Start()
    {
        FillParty();
        ActiveGui();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGui();
    }
    // chiamare al cambio party
    private void FillParty(){
        Nparty=0;
        PlayerController mPlayer = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i<players.Count() ; i++)
        {
            mPlayer = players[i].GetComponent<PlayerController>();
            if (mPlayer.inParty){
                Nparty++;
                if(mPlayer.used)
                    player = mPlayer;
            }
        }
        party = player.players;
        UpdateFace();
    }


    private void UpdateGui(){
        try{
            if (Nparty>1){
                pg1.transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>().value = party[1].gameObject.GetComponent<PlayerManager>().health / party[1].gameObject.GetComponent<PlayerManager>().maxHealth;
                pg1.transform.GetChild(1).transform.GetChild(1).GetComponent<Slider>().value = party[1].gameObject.GetComponent<PlayerManager>().hunger / party[1].gameObject.GetComponent<PlayerManager>().maxHunger;
                pg1.transform.GetChild(1).transform.GetChild(2).GetComponent<Slider>().value = party[1].gameObject.GetComponent<PlayerManager>().water / party[1].gameObject.GetComponent<PlayerManager>().maxWater;
                    if(Nparty>2){
                    pg2.transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>().value = party[2].gameObject.GetComponent<PlayerManager>().health / party[2].gameObject.GetComponent<PlayerManager>().maxHealth;
                    pg2.transform.GetChild(1).transform.GetChild(1).GetComponent<Slider>().value = party[2].gameObject.GetComponent<PlayerManager>().hunger / party[2].gameObject.GetComponent<PlayerManager>().maxHunger;
                    pg2.transform.GetChild(1).transform.GetChild(2).GetComponent<Slider>().value = party[2].gameObject.GetComponent<PlayerManager>().water / party[2].gameObject.GetComponent<PlayerManager>().maxWater;
                        if(Nparty>3){
                        pg3.transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>().value = party[3].gameObject.GetComponent<PlayerManager>().health / party[3].gameObject.GetComponent<PlayerManager>().maxHealth;
                        pg3.transform.GetChild(1).transform.GetChild(1).GetComponent<Slider>().value = party[3].gameObject.GetComponent<PlayerManager>().hunger / party[3].gameObject.GetComponent<PlayerManager>().maxHunger;
                        pg3.transform.GetChild(1).transform.GetChild(2).GetComponent<Slider>().value = party[3].gameObject.GetComponent<PlayerManager>().water / party[3].gameObject.GetComponent<PlayerManager>().maxWater;
                        }
                    } 
            }
        }catch{}
    }

    private void ActiveGui(){
        if (Nparty>1){
                pg1.gameObject.SetActive(true);
            if(Nparty>2){
                    pg2.gameObject.SetActive(true);
                if(Nparty>3){
                        pg3.gameObject.SetActive(true);
                }
            }      
        }
        bars = GameObject.FindGameObjectsWithTag("Bar");
        //ActiveBars();
    }

    public void ActiveBars(){
        if (Nparty>1){
            if(bars[0].activeSelf)
                bars[0].gameObject.SetActive(false);
            else
                bars[0].gameObject.SetActive(true);
            if(Nparty>2){
                if(bars[1].activeSelf)
                    bars[1].gameObject.SetActive(false);
                else
                    bars[1].gameObject.SetActive(true);
                if(Nparty>3){
                    if(bars[2].activeSelf)
                        bars[2].gameObject.SetActive(false);
                     else
                        bars[2].gameObject.SetActive(true);
                }
            }      
        }
    }

    private void UpdateFace(){
        if (Nparty>1){
            pg1.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite=party[1].gameObject.GetComponent<PlayerController>().face;
            if(Nparty>2){
                pg2.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite=party[2].gameObject.GetComponent<PlayerController>().face;
                if(Nparty>3){
                    pg3.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite=party[3].gameObject.GetComponent<PlayerController>().face;
                }
            }
        }
    }
}

