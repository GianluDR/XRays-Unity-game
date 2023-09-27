using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PauseMenuScript : MonoBehaviour
{

    public bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Button resumeButton;
    private PlayerInput player;

    // Update is called once per frame
    void Start()
    {
        /*if (Instance != null)
            Destroy(this);
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);*/
    }

    void Update()
    {
    }

    private void OnPause()
    {
        if (GameIsPaused)
            Resume();
        else
            Pause();
            
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<AudioManager>().stopPauseAll();
    }

    public void Pause()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
            resumeButton.Select();
            resumeButton.OnSelect(null);
            FindObjectOfType<AudioManager>().pauseAll();
        }
    }

    private void searchPlayer(){
        PlayerInput mPlayer = null;
        PlayerController mPlayerController = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i<players.Count() ; i++)
        {
            mPlayer = players[i].GetComponent<PlayerInput>();
            mPlayerController = players[i].GetComponent<PlayerController>();
            if (mPlayerController.used)
            {
                player = mPlayer;
            }
        }
    }

    public void OptionPause()
    {

    }

    public void GoMenu()
    {
        Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
        SceneManager.LoadScene("MainMenu");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player.enabled = true;
    }



    public static bool speedato = false;

    private void OnSpeed()
    {
        if(speedato==true)
            piano();
        else
            veloce();
    }

    void veloce()
    {
        Time.timeScale = 35f;
        speedato = true;

    }

    void piano()
    {
        Time.timeScale = 1f;
        speedato = false;
    }

}
