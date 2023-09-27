using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseButtonSounds : MonoBehaviour, ISelectHandler
{

    bool firstTime;
    string ButtonName;
    public Button thisButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        firstTime = true;
        ButtonName = gameObject.name;
        thisButton.onClick.AddListener(TaskOnClick);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (ButtonName != "ResumeButton")
            firstTime = false;

        if(!firstTime)
            FindObjectOfType<AudioManager>().Play("OnSelectMenu");
        Debug.Log(firstTime);
        firstTime = false;
    }

    void TaskOnClick()
    {
        if(ButtonName == "GoMenuButton")
            FindObjectOfType<AudioManager>().Play("OnQuitGame");
        else if(ButtonName == "BackButton")
            FindObjectOfType<AudioManager>().Play("OnBackMenu");
        else
        FindObjectOfType<AudioManager>().Play("OnClickMenu");
    }

}