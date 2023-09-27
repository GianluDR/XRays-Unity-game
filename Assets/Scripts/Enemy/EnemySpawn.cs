using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    void start(){
        while(true){
            InvokeRepeating("spawn", 2.0f, 5f);
        }
    }

    public GameObject skelPrefab;
    public Transform[] spawnPoint;
    public void spawn(){
        for(int i = 0;i<spawnPoint.Count();i++){
            Debug.Log(spawnPoint[i].position);
            Instantiate(skelPrefab, spawnPoint[i].position, spawnPoint[i].rotation);
        }
    }
}
