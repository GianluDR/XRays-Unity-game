using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    int[] rotations = { 0,90,180,270 };

    public float[] correctRotation;
    [SerializeField]
    bool isPlaced = false;

    int possibleRots = 1;

    public PuzzleManager PuzzleManager;

    // Start is called before the first frame update
    private void Start()
    {
        possibleRots = correctRotation.Length;
        int rand = Random.Range(0,rotations.Length);
        transform.eulerAngles = new Vector3(0, 0, rotations[rand]);
        if(possibleRots > 1){
            if((transform.eulerAngles.z-0.1 < correctRotation[0] 
                && transform.eulerAngles.z+0.1 > correctRotation[0])||
                (transform.eulerAngles.z-0.1 < correctRotation[1] 
                && transform.eulerAngles.z+0.1 > correctRotation[1])){
                isPlaced = true;
                PuzzleManager.correctMove();
            }        
        }else{
            if(transform.eulerAngles.z-0.1 < correctRotation[0] 
                && transform.eulerAngles.z+0.1 > correctRotation[0]){
                isPlaced = true;
                PuzzleManager.correctMove();
            } 
        }
    
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Rotate(){
        /*Quaternion rotation = transform.localRotation;
        rotation.Set(rotation.x,rotation.y,rotations[i],rotation.w);
        transform.SetPositionAndRotation(transform.position,rotation);*/
        transform.rotation = Quaternion.Euler(0, 0, 90.0f) * transform.rotation;
        if(possibleRots > 1){
            if((transform.eulerAngles.z-0.1 < correctRotation[0] 
                && transform.eulerAngles.z+0.1 > correctRotation[0])
                || (transform.eulerAngles.z-0.1 < correctRotation[1] 
                && transform.eulerAngles.z+0.1 > correctRotation[1])
                && isPlaced == false){
                isPlaced = true;
                PuzzleManager.correctMove();
            }else if(isPlaced == true){
                isPlaced = false;
                PuzzleManager.wrongMove();
            }
        }else{
            if((transform.eulerAngles.z-0.1 < correctRotation[0] 
                && transform.eulerAngles.z+0.1 > correctRotation[0])
                && isPlaced == false){
                isPlaced = true;
                PuzzleManager.correctMove();
            }else if(isPlaced == true){
                isPlaced = false;
                PuzzleManager.wrongMove();
            }
        }
    }
}
