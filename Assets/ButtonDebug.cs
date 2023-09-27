using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ButtonDebug : MonoBehaviour
{
    Button testt;
    // Start is called before the first frame update
    void Start()
    {
        /*testt = GameObject.Find("ButtonPgUL").GetComponent<Button>();
        testt.Select();
        testt.OnSelect(null);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test(){
        Debug.Log("test");
    }
}
