using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        
        FindObjectOfType<AudioManager>().StopFadeOut("CutsceneMusic",2f);
        FindObjectOfType<AudioManager>().PlayFadeIn("GameMusic",0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
