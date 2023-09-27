using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButtonsSounds : MonoBehaviour, ISelectHandler
{
    bool firstTime;
    string ButtonName;
    public Button thisButton;

    void Awake()
    {
        firstTime = true;
        ButtonName = gameObject.name;
        thisButton.onClick.AddListener(TaskOnClick);
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (ButtonName != "PlayButton")
            firstTime = false;

        if(!firstTime)
            FindObjectOfType<AudioManager>().Play("OnSelectMenu");

        firstTime = false;
    }

    void TaskOnClick()
    {
        if(ButtonName == "YesButton")
            FindObjectOfType<AudioManager>().Play("OnQuitGame");
        else if(ButtonName == "NoButton" || ButtonName == "BackButton")
            FindObjectOfType<AudioManager>().Play("OnBackMenu");
        else
        FindObjectOfType<AudioManager>().Play("OnClickMenu");
    }

    public void SetFirstTime()
    {
        firstTime = true;
    }

    public void quit(){
        Application.Quit();
    }
}
