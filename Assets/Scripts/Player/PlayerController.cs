using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool inParty;
    public bool used;
    public float moveSpeed = 2f;
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public Animator animator;
    public static PlayerController Instance;
    private PlayerManager stats;
    public PgStatsScript pgStats;
    public Vector2 tpPosition;
    public Inventory inventory;
    public DialogueManager dialogue;
    public Sword mySword;
    public Item myItem;
    public int mySlot = 0;
    public Sprite face;
    public HUD hud;
    public bool canMove;
    public List<GameObject> players;
    public Vector2 movementInput;
    public PauseMenuScript pause;
    public GameObject Quest;
    public Sprite QuestTick;
    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerManager>();
        Instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            if(Instance != null)
                Destroy(this);
        }
        if( animator.GetBool("IsMoving") && (!pause.GameIsPaused)){
            try{
                if(SceneManager.GetActiveScene().name != "Labirint")
                    FindObjectOfType<AudioManager>().waitPlaying("Footstep");
                else
                    FindObjectOfType<AudioManager>().waitPlaying("concreteFootstep");
            }catch{}
        }
        checkQuest();
    }

    public void checkQuest(){
        if(Quest.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite == QuestTick &&
            Quest.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite == QuestTick &&
            Quest.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite == QuestTick){
                SceneManager.LoadScene(9);
        }
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                //test only x and y for sliding to collision objects
                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                    if (!success)
                    {
                        success = TryMove(new Vector2(0, movementInput.y));
                    }
                }
                animator.SetFloat("Horizontal", movementInput.x);
                animator.SetFloat("Vertical", movementInput.y);
                animator.SetBool("IsMoving", success);
            }
            else
                animator.SetBool("IsMoving", false);
        }
    }

    public void OnPause()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            if (pause.GameIsPaused)
                pause.Resume();
            else
                pause.Pause(); 
        }
    }

    public void OnSpeed()
    {
        if(Time.timeScale>1)
            Time.timeScale = 1f;
        else
            Time.timeScale = 35f;
    }

    public void OnInteraction()
    {
        if (mNpcToTalk != null)
        {
            if(dialogue.dialoguePanel.activeSelf)
                dialogue.ContinueStory();
            else
                dialogue.EnterDialogueMode(mNpcToTalk,false);
        }
        if (mWater != null)
        {
            stats.Drink(mWater.getWater());
        }
        if(mPuzzleToDo != null)
        {
            if(mPuzzleToDo.checkItems())
                mPuzzleToDo.startMinigame();
            else
                Debug.Log("NON HAI GLI ITEMS GIUSTI");
        }
        if(mWall != null)
        {
            if(!mWall.repaired){
                mWall.Repair(this);
                hud.CloseInteractionPanel("");
                mWall.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }else
                Debug.Log("Riparato");
        }
    }

    /*private void OnFocus()
    {
        if(mPuzzleToDo != null)
        {
            if(mPuzzleToDo.checkItems())
                mPuzzleToDo.startMinigame();
            else
                Debug.Log("NON HAI GLI ITEMS GIUSTI");
        }
    }*/

    public void OnPickup()
    {
        if(mItemToPickup != null)
        {
            inventory.AddItem(mItemToPickup);
        }
    }

    public void OnDrop()
    {
        inventory.DropItem();
    }

    private bool questView = true;
    public void OnStats(){
        pgStats.ActiveBars();
        if(questView){
            Quest.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Quest.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            Quest.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(false);
            questView = false;
        }else{
            Quest.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Quest.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Quest.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.SetActive(true);
            questView = true;
        }
    }

    public void OnHoverLeft()
    {
        inventory.GoHoverLeft();
    }

    public void OnHoverRight()
    {
        inventory.GoHoverRight();
    }

    public void OnToSlot0()
    {
        inventory.ToSlot0();
    }

    public void OnToSlot1()
    {
        inventory.ToSlot1();
    }

    public void OnToSlot2()
    {
        inventory.ToSlot2();
    }

    public void OnToSlot3()
    {
        inventory.ToSlot3();
    }

    /*private void OnHoverChange()
    {
        if (Mouse.current.scroll.ReadValue().normalized == new Vector2(0, 1))
            inventory.GoHoverLeft();
        else if(Mouse.current.scroll.ReadValue().normalized == new Vector2(0, -1))
            inventory.GoHoverRight();
    }*/

    private IInventoryItem mItemToPickup = null;
    private PuzzleTrigger mPuzzleToDo = null;
    private DialogueTrigger mNpcToTalk = null;
    private WaterPurifier mWater = null;
    private WallToRepair mWall = null;
    private void OnTriggerEnter2D(Collider2D hit)
    {
        GameObject trigImgPz;
        SpriteRenderer trigImgNpc;
        IInventoryItem item = hit.gameObject.GetComponent<IInventoryItem>();
        if(item != null)
        {
            mItemToPickup = item;
            hud.OpenInstructionPanel("");
        }

        DialogueTrigger npc = hit.gameObject.GetComponent<DialogueTrigger>();
        if (npc != null)
        {
            trigImgNpc = npc.gameObject.GetComponent<SpriteRenderer>();
            trigImgNpc.enabled = true;
            mNpcToTalk = npc;
            hud.OpenInteractionPanel("");
        }

        PuzzleTrigger puzzle = hit.gameObject.GetComponent<PuzzleTrigger>();
        if (puzzle != null)
        {
            if(!puzzle.puzzleSolved){
                trigImgPz = puzzle.gameObject.transform.GetChild(0).gameObject;
                trigImgPz.SetActive(true);
                mPuzzleToDo = puzzle;
                hud.OpenInteractionPanel("");
            }
        }
        WaterPurifier water = hit.gameObject.GetComponent<WaterPurifier>();
        SpriteRenderer trigImgWater = null;
        if (water != null)
        {
            if(water.repaired){
                trigImgWater = water.gameObject.GetComponent<SpriteRenderer>();
                trigImgWater.enabled = true;
                hud.OpenInteractionPanel("");
            }
            mWater = water;
        }
        WallToRepair wall = hit.gameObject.GetComponent<WallToRepair>();
        if (wall != null)
        {
            if(!wall.repaired){
                wall.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                hud.OpenInteractionPanel("");
            }
            mWall = wall;
        }
    }

    private void OnTriggerStay2D(Collider2D hit)
    {
        GameObject trigImgPz;
        SpriteRenderer trigImgNpc;
        IInventoryItem item = hit.gameObject.GetComponent<IInventoryItem>();
        if(item != null)
        {
            mItemToPickup = item;
            hud.OpenInstructionPanel("");
        }

        DialogueTrigger npc = hit.gameObject.GetComponent<DialogueTrigger>();
        if (npc != null)
        {
            trigImgNpc = npc.gameObject.GetComponent<SpriteRenderer>();
            trigImgNpc.enabled = true;
            mNpcToTalk = npc;
            hud.OpenInteractionPanel("");
        }

        PuzzleTrigger puzzle = hit.gameObject.GetComponent<PuzzleTrigger>();
        if (puzzle != null)
        {
            if(!puzzle.puzzleSolved){
                trigImgPz = puzzle.gameObject.transform.GetChild(0).gameObject;
                trigImgPz.SetActive(true);
                mPuzzleToDo = puzzle;
                hud.OpenInteractionPanel("");
            }
        }
        WaterPurifier water = hit.gameObject.GetComponent<WaterPurifier>();
        SpriteRenderer trigImgWater = null;
        if (water != null)
        {
            water.checkStatus();
            if(water.repaired){
                trigImgWater = water.gameObject.GetComponent<SpriteRenderer>();
                trigImgWater.enabled = true;
                hud.OpenInteractionPanel("");
            }
            mWater = water;
        }
        WallToRepair wall = hit.gameObject.GetComponent<WallToRepair>();
        if (wall != null)
        {
            if(!wall.repaired){
                wall.gameObject.transform.GetChild(2).gameObject.SetActive(true);
                hud.OpenInteractionPanel("");
            }
            mWall = wall;
        }
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        GameObject trigImgPz;
        SpriteRenderer trigImgNpc;
        IInventoryItem item = hit.gameObject.GetComponent<IInventoryItem>();
        if(item != null)
        {
            hud.CloseInstructionPanel("");
            mItemToPickup = null;
        }

        DialogueTrigger npc = hit.gameObject.GetComponent<DialogueTrigger>();
        if (npc != null)
        {
            trigImgNpc = npc.gameObject.GetComponent<SpriteRenderer>();
            try{
                dialogue.ExitDialogueMode();
            }catch{}
            trigImgNpc.enabled = false;
            mNpcToTalk = null;
            hud.CloseInteractionPanel("");
        }

        PuzzleTrigger puzzle = hit.gameObject.GetComponent<PuzzleTrigger>();
        if (puzzle != null)
        {
            if(!puzzle.puzzleSolved){
                trigImgPz = puzzle.gameObject.transform.GetChild(0).gameObject;
                trigImgPz.SetActive(false);
                mPuzzleToDo = null;
                hud.CloseInteractionPanel("");
            }
        }
        WaterPurifier water = hit.gameObject.GetComponent<WaterPurifier>();
        SpriteRenderer trigImgWater = null;
        if (water != null)
        {
            if(water.repaired){
                trigImgWater = water.gameObject.GetComponent<SpriteRenderer>();
                trigImgWater.enabled = false;
                hud.CloseInteractionPanel("");
            }
            mWater = null;
        }
        WallToRepair wall = hit.gameObject.GetComponent<WallToRepair>();
        if (wall != null)
        {
            if(!wall.repaired){
                wall.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                hud.CloseInteractionPanel("");
            }
            mWall = null;
        }
    }

    public void OnFire()
    {
        if(!pause.GameIsPaused)
            animator.SetTrigger("Attack");
    }

    /*public void OnPlayerSwap()
    {
        GameObject player = players[0];
        GameObject camera = players[0].transform.GetChild(0).gameObject;
        PlayerController playerController = players[0].GetComponent<PlayerController>();
        PlayerInput playerInput = players[0].GetComponent<PlayerInput>();
        camera.SetActive(false);
        playerInput.enabled = false;
        playerController.used = false;
        
        int i;
        for(i = 1;i < players.Count();i++){
            if(players[i] != null){
                GameObject swap = players[i];
                players[i] = player;
                player = swap;
            }
        }
        players[0] = player;

        camera = player.transform.GetChild(0).gameObject;
        playerController = player.GetComponent<PlayerController>();
        playerInput = player.GetComponent<PlayerInput>();
        camera.SetActive(true);
        playerInput.enabled = true;
        playerController.used = true;

        for(i = 0;i < players.Count()-1;i++){
            if(players[i] != null){
                playerController = players[i].GetComponent<PlayerController>();
                playerController.players = players;
            }
        }
    }*/

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                    direction,
                    movementFilter,
                    castCollisions,
                    moveSpeed
                        * Time.fixedDeltaTime
                        + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(
                    rb.position
                    + direction
                    * moveSpeed
                    * Time.fixedDeltaTime);
                return true;
            }
            else {
                return false;
            }
        }
        else return false;
    }

    public void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    public void enableMove()
    {
        mySword.StopAttack();
        canMove = true;
    }

    public void disableMove(string direction)
    {
        canMove = false;
        mySword.Attack(direction);
    }

}
