using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerInputContr : MonoBehaviour
{
    public List<GameObject> players;
    private PlayerController playerUsed;
    
    // Start is called before the first frame update
    void Start()
    {
        playerUsed = players[0].GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPause()
    {
        playerUsed.OnPause();
    }

    public void OnSpeed()
    {
        playerUsed.OnSpeed();
    }

    public void OnInteraction()
    {
        playerUsed.OnInteraction();
    }

    /*private void OnFocus()
    {
        if(mPuzzleToDo != null)
        {
            if(mPuzzleToDo.checkItems())
                mPuzzleToDo.startMinigame();
            else
                Debug.Log("NON HAI GLI ITEMS GIUSTI");
        }
    }*/

    public void OnPickup()
    {
        playerUsed.OnPickup();
    }

    public void OnDrop()
    {
        playerUsed.OnDrop();
    }

    public void OnStats(){
        playerUsed.OnStats();
    }

    public void OnHoverLeft()
    {
        playerUsed.OnHoverLeft();
    }

    public void OnHoverRight()
    {
        playerUsed.OnHoverRight();
    }

    public void OnToSlot0()
    {
        playerUsed.OnToSlot0();
    }

    public void OnToSlot1()
    {
        playerUsed.OnToSlot1();
    }

    public void OnToSlot2()
    {
        playerUsed.OnToSlot2();
    }

    public void OnToSlot3()
    {
        playerUsed.OnToSlot3();
    }

    /*private void OnHoverChange()
    {
        if (Mouse.current.scroll.ReadValue().normalized == new Vector2(0, 1))
            inventory.GoHoverLeft();
        else if(Mouse.current.scroll.ReadValue().normalized == new Vector2(0, -1))
            inventory.GoHoverRight();
    }*/

    public void OnFire()
    {
        playerUsed.OnFire();
    }

    public void OnPlayerSwap()
    {
        playerUsed.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = false;
        playerUsed.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        playerUsed = players[1].GetComponent<PlayerController>();
        playerUsed.gameObject.transform.GetChild(0).gameObject.GetComponent<Camera>().enabled = true;
        playerUsed.gameObject.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OnMove(InputValue movementValue)
    {
        playerUsed.OnMove(movementValue);
    }

}
