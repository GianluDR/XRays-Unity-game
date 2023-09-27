using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    public IEnumerator Start(){
        Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
        yield return new WaitForSeconds(5f);
        FindObjectOfType<AudioManager>().StopPlaying("GameMusic");
        SceneManager.LoadScene(0);
    }
}
