using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevel : MonoBehaviour 
{

    public int levelToLoad;
    public Vector2 targetPosition;
    //public VectorValue playerStorage;
    //public VectorValue positionToLoad;

    public Animator transition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Player")
        {
            PlayerController.Instance.tpPosition = targetPosition;
            LoadScene();
        }
    }

    public void LoadScene()
    {
        //transition.SetTrigger("start");
        SaveManager save = ScriptableObject.CreateInstance<SaveManager>();
        save.SaveData();
        Debug.Log(save);
        if(levelToLoad==7){
            FindObjectOfType<AudioManager>().StopFadeOut("GameMusic",2f);
            FindObjectOfType<AudioManager>().PlayFadeIn("LabyrinthMusic",0.005f);
            TutorialAtt();
        }
        else{
            try{
            FindObjectOfType<AudioManager>().StopFadeOut("LabyrinthMusic",2f);
            FindObjectOfType<AudioManager>().waitPlaying("GameMusic");
            }catch{}
        }
        SceneManager.LoadScene(levelToLoad);
    }

    public IEnumerator TutorialAtt(){
        GameObject tutorialAtt = GameObject.Find("Tutorial").transform.GetChild(1).gameObject;
        tutorialAtt.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialAtt.SetActive(false);
    }

}
