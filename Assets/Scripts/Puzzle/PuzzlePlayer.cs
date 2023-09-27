using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzlePlayer : MonoBehaviour
{
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    Rigidbody2D rb;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public PuzzleTrigger puzzleTrig;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRight(){
        if(TryMove(new Vector3(0.16f,0))){
            this.gameObject.transform.Translate(new Vector3(0.16f,0));
            FindObjectOfType<AudioManager>().Play("OnMovePuzzle");
        }
    }

    public void OnLeft(){
        if(TryMove(new Vector3(-0.16f,0))){
            this.gameObject.transform.Translate(new Vector3(-0.16f,0));
            FindObjectOfType<AudioManager>().Play("OnMovePuzzle");
        }   
    }

    public void OnUp(){
        if(TryMove(new Vector3(0,0.16f))){
            this.gameObject.transform.Translate(new Vector3(0,0.16f));
            FindObjectOfType<AudioManager>().Play("OnMovePuzzle");
        }   
    }

    public void OnDown(){
        if(TryMove(new Vector3(0,-0.16f))){
            this.gameObject.transform.Translate(new Vector3(0,-0.16f));
            FindObjectOfType<AudioManager>().Play("OnMovePuzzle");
        }   
    }

    public void OnRotation(){
        try{
            mPipe.Rotate();
            FindObjectOfType<AudioManager>().Play("OnRotatePuzzle");
        }catch{}
    }

    public void OnExit(){
        puzzleTrig.exitFromPuzzle();
        FindObjectOfType<AudioManager>().Play("OnBackMenu");
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(
                    rb.position
                    + direction);
                return  true;
            }
            else {
                return false;
            }
        }
        else return false;
    }

    private Pipes mPipe = null;
    /*private void OnTriggerEnter2D(Collider2D hit)
    {
        Pipes pipe = hit.gameObject.GetComponent<Pipes>();
        if(pipe != null)
        {
            mPipe = pipe;
        }
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        Pipes pipe = hit.gameObject.GetComponent<Pipes>();
        if(pipe != null)
        {
            mPipe = null;
        }
    }*/
    public void OnTriggerStay2D(Collider2D hit)
    {
        Pipes pipe = hit.gameObject.GetComponent<Pipes>();
        if(pipe != null)
        {
            mPipe = pipe;
        }else mPipe = null;
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        Pipes pipe = hit.gameObject.GetComponent<Pipes>();
        if(pipe != null)
        {
            mPipe = null;
        }
    }
}
