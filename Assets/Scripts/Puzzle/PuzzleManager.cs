using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public GameObject PipesHolder;
    public GameObject[] Pipes;
    public PuzzleTrigger trigger;

    private Animator animator;
    
    [SerializeField]
    public int totalPipes = 0;

    [SerializeField]
    int correctedPipes = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        totalPipes = PipesHolder.transform.childCount-2;
        Pipes = new GameObject[totalPipes];

        for(int i = 2; i < Pipes.Length+2; i++){
            Pipes[i-2] = PipesHolder.transform.GetChild(i).gameObject;
        }
    }

    public void correctMove()
    {
        correctedPipes += 1;
        if(correctedPipes == totalPipes){
            animator.SetTrigger("Win");
            StartCoroutine(trigger.win());
        }
    }

    public void wrongMove()
    {
        correctedPipes -= 1;
    }
}
