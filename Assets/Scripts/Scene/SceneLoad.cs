using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private Transform player; //drag player reference onto here

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded; //You add your method to the delegate
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    //After adding this method to the delegate, this method will be called every time
    //that a new scene is loaded. You can then compare the scene loaded to your desired
    //scenes and do actions according to the scene loaded.
    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 0){
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            try{
                Destroy(players[1]);
            }catch{}
        }
        if(scene.buildIndex == 2)
        {
            GameObject.Find("Thorne").SetActive(true);
            GameObject.Find("Thorne").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Thorne").GetComponent<PlayerController>().enabled = true;
            GameObject.Find("Thorne").GetComponent<PlayerInput>().enabled = true;
            GameObject.Find("Thorne").transform.GetChild(0).gameObject.SetActive(true);
        }
        try { 
            player.position = PlayerController.Instance.tpPosition;
        }
        catch { /*NullReferenceException here*/ }
        SaveManager save = ScriptableObject.CreateInstance<SaveManager>();
        save.RestoreData();
    }

}
