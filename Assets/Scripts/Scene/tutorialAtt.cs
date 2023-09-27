using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tutorialAtt : MonoBehaviour
{
    public IEnumerator Start(){
        GameObject tutorialAtt = GameObject.Find("Tutorial").transform.GetChild(1).gameObject;
        Debug.Log(tutorialAtt);
        tutorialAtt.SetActive(true);
        yield return new WaitForSeconds(5);
        tutorialAtt.SetActive(false);
    }
}
