using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScriptMenuSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnEnable()
    {
        Button SelectOptions = GameObject.Find("PlayButton").GetComponent<Button>();
        SelectOptions.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
