using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {      
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void logoStart(){
        FindObjectOfType<AudioManager>().PlayFadeIn("Meow",0.05f);
    }

    public void startMusic(){
        FindObjectOfType<AudioManager>().PlayFadeIn("MainMenuMusic",0.05f);
        Button SelectOptions = GameObject.Find("PlayButton").GetComponent<Button>();
        SelectOptions.Select();
        TextMeshProUGUI TextPro = GameObject.Find("PlayButtonText").GetComponent<TextMeshProUGUI>();
        TextPro.text = ">Gioca";
    }

    public void PlayScene()
    {
        SceneManager.LoadScene("GameStart");
        PlayerPrefs.DeleteAll();
        FindObjectOfType<AudioManager>().StopPlaying("MainMenuMusic");
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>().tpPosition = Vector2.zero;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
