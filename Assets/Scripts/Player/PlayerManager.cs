using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    [Header("Player Health")]
    public float maxHealth;
    public float health;
    public Slider healthSlider;

    [Header("Player Hunger")]
    public float maxHunger;
    public float hunger;
    public float hungerOT = 2f;
    public Slider hungerSlider;

    [Header("Player Water")]
    public float maxWater;
    public float water; 
    public float waterOT = 3f;
    public Slider waterSlider;

    [Header("Player Rad")]
    public float maxRad;
    public float rad; 
    public float radHeal = 3f;
    public Slider radSlider;

    private bool isRad;

    private static PlayerManager Instance;

    
    private SpriteRenderer spriteR;

    void Awake()
    {   

        Instance = this;
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "GameStart"){
            if (this.gameObject.GetComponent<PlayerController>().animator.GetBool("IsMoving"))
            {
                hunger = hunger - hungerOT * Time.deltaTime;
            }
            water = water - waterOT * Time.deltaTime;
            updateHunger();
            updateWater();
            if(hunger <= 0 || water <= 0 || rad == 100){
                health = health - 1f * Time.deltaTime;
            }
            updateHealth();
            if(health <= 0){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                FindObjectOfType<AudioManager>().Play("gameOver");
                health = maxHealth;
                hunger = maxHunger;
                water = maxWater;
                rad = maxRad;
                updateRad();
                updateWater();
                updateHunger();
                updateHealth();
            }
            if(!isRad){
                if(rad > 0){
                    radHealing();
                }
            }
        }
    }

    void updateHunger(){hungerSlider.value = hunger / maxHunger;}
    void updateWater(){waterSlider.value = water / maxWater;}
    void updateHealth(){healthSlider.value = health / maxHealth;}
    void updateRad(){radSlider.value = rad / maxRad;}

    public void TakeDamage(float damage){
        health = health - damage;
        updateHealth();
        if(health <= 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            FindObjectOfType<AudioManager>().Play("gameOver");
            health = maxHealth;
            hunger = maxHunger;
            water = maxWater;
            rad = maxRad;
            updateRad();
            updateWater();
            updateHunger();
            updateHealth();
        }
        spriteR= GetComponent<SpriteRenderer>();
        StartCoroutine(damageLight());
    }

    IEnumerator damageLight(){
        yield return new WaitForSeconds(0.5f);
        spriteR.color= new Color(.3f, .3f, .3f, 1f);
        yield return new WaitForSeconds(0.25f);
        spriteR.color= new Color(1f, 1f, 1f, 1f);
    }

    public void Drink(float drinked){
        water = water + drinked;
        if(water > maxWater)
            water = maxWater;
        updateWater();
    }

    private void radHealing(){
        rad = rad - radHeal * Time.deltaTime;
        if(rad <= 0){
            rad = 0;
            radSlider.gameObject.SetActive(false);
        }
        updateRad();
    }

    public void radEffect(float radOT){
        rad = rad + radOT * Time.deltaTime;
        if(rad > maxRad)
            rad = maxRad;
        radSlider.gameObject.SetActive(true);
        isRad = true;
        updateRad();
    }

    public void stopRad(){
        isRad = false;
    }
}
