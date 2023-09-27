using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScene : MonoBehaviour
{
    private DialogueTrigger block;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        block = GameObject.Find("BlockScene").GetComponent<DialogueTrigger>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.tag == "Player")
        {
            DialogueManager dialogue = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
            dialogue.EnterDialogueMode(block,false);
            player = collisionGameObject;  
            yield return StartCoroutine(wait()); 
            block.SetPoint(0);
            if(this.name == "blockW"){
                player.transform.position = new Vector3(player.transform.position.x,
                                                        player.transform.position.y - 0.16f,
                                                        player.transform.position.z);
            }
            else if(this.name == "blockS"){
                player.transform.position = new Vector3(player.transform.position.x,
                                                        player.transform.position.y + 0.16f,
                                                        player.transform.position.z);
            }
            else if(this.name == "blockA"){
                player.transform.position = new Vector3(player.transform.position.x + 0.16f,
                                                        player.transform.position.y,
                                                        player.transform.position.z);
            }
            else if(this.name == "blockD"){
                player.transform.position = new Vector3(player.transform.position.x - 0.16f,
                                                        player.transform.position.y,
                                                        player.transform.position.z);
            }  
        }
    }

    private IEnumerator wait()
    {
        while(block.GetPoint() != 1){
            yield return null;
        }
    }

}
