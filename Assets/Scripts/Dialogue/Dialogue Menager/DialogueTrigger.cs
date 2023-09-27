using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;

    private int point = 0;
    public int pointMAX;
    public bool crossfade = false;

    public Sprite npcFace;

    public void SetPoint(int i)
    {
        point = i;
    }

    public int GetPoint()
    {
        return point;
    }

    public void SetPointUp()
    {
        if(point < pointMAX)
            point++;
    }
}