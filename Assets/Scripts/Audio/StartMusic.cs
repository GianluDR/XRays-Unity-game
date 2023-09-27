using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayFadeIn("Whoosh",0.5f);
        FindObjectOfType<AudioManager>().PlayFadeIn("CutsceneMusic",0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
