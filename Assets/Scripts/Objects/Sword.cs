using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 1;
    private Vector2 sideLOffset;
    private Vector2 sideUOffset;
    public CapsuleCollider2D swordHitBox;

    // Start is called before the first frame update
    void Start()
    {
        sideLOffset = new Vector2(-0.28f, -0.138f);
        sideUOffset = new Vector2(0f, 0.4f);
    }

    public void Attack(string direction){
        FindObjectOfType<AudioManager>().Play("SwordHit");
        switch(direction){
            case "left":
                AttackLeft();
                break;
            case "right":
                AttackRight();
                break;
            case "up":
                AttackUp();
                break;
            case "down":
                AttackDown();
                break;   
        }
    }

    private void AttackRight() {
        swordHitBox.direction = CapsuleDirection2D.Horizontal;
        swordHitBox.enabled = true;
        swordHitBox.offset = new Vector3(sideLOffset.x * -1, sideLOffset.y);
    }

    private void AttackLeft()
    {
        swordHitBox.direction = CapsuleDirection2D.Horizontal;
        swordHitBox.enabled = true;
        swordHitBox.offset = sideLOffset;
    }

    private void AttackUp()
    {
        swordHitBox.direction = CapsuleDirection2D.Vertical;
        swordHitBox.enabled = true;
        swordHitBox.offset = sideUOffset;
    }

    private void AttackDown()
    {
        swordHitBox.direction = CapsuleDirection2D.Vertical;
        swordHitBox.enabled = true;
        swordHitBox.offset = new Vector3(sideUOffset.x, sideUOffset.y * -1);
    }
    
    public void StopAttack()
    {
        swordHitBox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D hit){
        Enemy mEnemy = hit.gameObject.GetComponent<Enemy>();
        if(mEnemy != null){
            mEnemy.TakeDamage(damage);
        }
    }
}
