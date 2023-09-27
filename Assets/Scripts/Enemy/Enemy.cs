using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool canMove = true;
    public EnemyAttack Bite;
    public float speed;
    public float checkRadius;
    public float attackRadius;
    private bool isInChaseRange;
    private bool isInAttackRange;
    public bool shouldRotate;
    private bool triggerSound=false;
    public LayerMask whatIsPlayer;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer enemySprite;
    private Transform target;
    public Vector3 dir;
    private Vector2 movement;
    public float Health{
        set{
            health = value;
            if(health <= 0){
                Defeated();
            }
        }
        get{
            return health;
        }
    }
    public float health;

    private void Start(){
        rb = GetComponent<Rigidbody2D>();
        enemySprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update(){
        target = findTarget();

        isInChaseRange = Physics2D.OverlapCircle(transform.position,checkRadius,whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position,attackRadius,whatIsPlayer);
        try{
        dir = target.position - transform.position;
        }catch{}
        float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if(shouldRotate){
            animator.SetFloat("Horizontal",dir.x);
            animator.SetFloat("Vertical",dir.y);
        }
    }

    private void FixedUpdate(){
        if(canMove){
            if(movement != Vector2.zero){
                if(isInChaseRange && !isInAttackRange){
                    animator.SetFloat("Horizontal", movement.x);
                    animator.SetBool("IsMoving", true);
                    if(movement.x<0){
                        enemySprite.flipX = true;
                    }else if(movement.x>0){
                        enemySprite.flipX = false;
                    }
                    MoveCharacter(movement);
                }
                if(isInAttackRange){
                    rb.velocity = Vector2.zero;
                    attack();
                }
            }else{
                animator.SetBool("IsMoving", false);
            }
        }
        if(isInChaseRange && !triggerSound){
            triggerSound=true;
            if(gameObject.tag=="Cobra"){
                    FindObjectOfType<AudioManager>().Play("CobraTrigger");
            }if(gameObject.tag=="Skeleton"){
                    FindObjectOfType<AudioManager>().Play("SkeletonTrigger");
            }
        }
        if(!isInChaseRange && triggerSound)
            triggerSound=false;
    }

    private void attack(){
        animator.SetTrigger("Attack");
            if(gameObject.tag=="Cobra")
                    FindObjectOfType<AudioManager>().Play("CobraAttack");
            if(gameObject.tag=="Skeleton")
                    FindObjectOfType<AudioManager>().Play("SkeletonAttack");
    }

    private void MoveCharacter(Vector2 dir){
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.fixedDeltaTime));
    }

    private Transform findTarget(){
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for(int i=0;i<players.Count();i++){
            if(players[i].GetComponent<PlayerController>().used){
                return players[i].transform;
            }
        }
        return null;
    }

    private void enableMove()
    {
        Bite.StopAttack();
        canMove = true;
    }

    private void disableMove()
    {
        canMove = false;
        string direction = null;
        if(movement.x<0){
            direction = "left";
        }else if(movement.x>0){
            direction = "right";
        }
        Bite.Attack(direction);
    }

    public void TakeDamage(float damage){
        Health -= damage;
        canMove = false;
        animator.SetTrigger("Hitted");
    }

    public void Defeated()
    {
        canMove = false;
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy(){
        Destroy(gameObject);
    }
}
