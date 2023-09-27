using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptExitConfirm : MonoBehaviour

{
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        Button Yes = GameObject.Find("YesButton").GetComponent<Button>();
        Yes.Select();
    }
    // Update is called once per frame
    void Update()
    {


    }
}
