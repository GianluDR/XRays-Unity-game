using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptOptionsSelect : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        Button SelectOptions = GameObject.Find("BackButton").GetComponent<Button>();
        SelectOptions.Select();
    }
        // Update is called once per frame
        void Update()
    {
        

    }
}
