using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float damage = 1;
    private Vector2 sideLOffset;
    public Collider2D HitBox;

    // Start is called before the first frame update
    void Start()
    {
        sideLOffset = new Vector2(0.21f, -0.045f);
    }

    public void Attack(string direction){
        switch(direction){
            case "left":
                AttackLeft();
                break;
            case "right":
                AttackRight();
                break;  
        }
    }

    private void AttackLeft() {
        HitBox.enabled = true;
        HitBox.offset = new Vector3(sideLOffset.x * -1, sideLOffset.y);
    }

    private void AttackRight(){
        HitBox.enabled = true;
        HitBox.offset = sideLOffset;
    }
    
    public void StopAttack(){
        HitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D hit){
        PlayerManager player = hit.gameObject.GetComponent<PlayerManager>();
        if(player != null){
            Debug.Log("test");
            player.TakeDamage(damage);
        }
    }
}

