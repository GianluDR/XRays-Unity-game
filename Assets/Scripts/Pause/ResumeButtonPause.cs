using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeButtonPause : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        Button ResumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        ResumeButton.Select();
    }
    // Update is called once per frame
    void Update()
    {


    }
}
