using System.Collections;
using System.Collections.Generic;
using InteractableObjects;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    
    private float moveSpeed = 12f;
    private float gravity = -9.81f;
    private Vector3 velocity;
    private float jumpHeight = 3f;
    private bool isGrounded;
    private float groundDistance = 0.4f;
    private int player_damage = 10;
    private int player_heal = 30;
    
    private List<GameObject> InteractableObjList = new List<GameObject>();
    private int PainKillersCollected, GoldCollected;
    private string input;
    public TMP_Text PainKillerCounter, GoldCounter;
    
    [SerializeField]
    private DomeHealScript[] domeHealScripts;
    public bool objectiveComplete = false;
    public LayerMask groundMask;
    public Transform groundCheck;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    MenuManager menuManager;
    public int health;
    public int max_health = 100;
    public bool displayFullHealthText = false;
    [SerializeField]
    private GunShot gunShot;
    [SerializeField]
    private HealthBar playerhealthBar;
    [SerializeField]
    private DoorScript doorScript;
    [SerializeField]
    private AudioSource coinCollectAudio;

    void Start()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Interactable")) 
        {
             InteractableObjList.Add(obj);
        }
        health = max_health;
        playerhealthBar.SetMaxHealth(max_health);
    }

    void Update()
    {
        //check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if((isGrounded) && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //movement of the player
        float NewX = Input.GetAxis("Horizontal");
        float NewZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * NewX + transform.forward * NewZ;
        controller.Move(move * moveSpeed * Time.deltaTime);
 
        //jump action of the player
        if(Input.GetButtonDown("Jump") && (isGrounded))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //shoot action of the player
        if(Input.GetKeyDown(KeyCode.Mouse0) && MenuManager.isGamePaused == false &&  !GetClosestDome().playerInDome)
        {
            gunShot.Shoot();
        }
        input = Input.inputString;

        //gets the nearest interactable object
        IInteractable interactable = CheckNearestObject();

        //interactable objects switch statement
        switch (input)
        {
            case "e":
                if (interactable == null) 
                {
                    return;
                }
                else
                {
                    interactable.Use();
                }
                break;
            case "r":
                if (interactable == null) 
                {
                    return;
                }
                else
                {
                    interactable.Collect();
                }
                break;
            //handles player using stored painkillers
            case "g":
                if (PainKillersCollected > 0 && health < 100)
                {
                    
                    PainKillerText(-1);
                    HealPlayer();
                }
                else if (PainKillersCollected > 0 && health >= 100)
                {
                    displayFullHealthText = true;
                }
                break;
        }   
    }

    //method called to heal the player
    public void HealPlayer()
    {
            displayFullHealthText = false;
            if (health >= 70)
            {
                health = 100;
            }
            else
            {
                health += player_heal;
            }
            playerhealthBar.SetHealth(health);
    }

    //if enemy bullet fired at player it will call the damage method
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "EnemyBullet" && health >= 0) TakeDamage();
    }

    //applies damage to player
    private void TakeDamage()
    {
        health -= player_damage;
        playerhealthBar.SetHealth(health);
        if (health <= 0) Invoke(nameof(DestroyPlayer), 0.5f);
    }

    //when player health goes to zero, this method is called
    private void DestroyPlayer()
    {
        playerhealthBar.DestroyHealthBar();
        menuManager.EnableDeadMenu();
    }

    //method returns the closest Gameobject from the list of interactable objects
    private GameObject GetClosestObject()
    {
        GameObject tObj = null;
        float minDist = 300f;
        Vector3 currentPos = transform.position;

        //compares each interactable object distance from player and returns the closest object in the range
        foreach (GameObject t in InteractableObjList)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tObj = t;
                minDist = dist;
            }
        }
        return tObj;
    }

    //returns the closest dome to player
    private DomeHealScript GetClosestDome()
    {
        DomeHealScript closestDome = null;
        float minDist = 300f;
        Vector3 currentPos = transform.position;
        foreach (DomeHealScript d in domeHealScripts)
        {
            float dist = Vector3.Distance(d.transform.position, currentPos);
            if (dist < minDist)
            {
                closestDome = d;
                minDist = dist;
            }
        }
        return closestDome;
    }

    //updates the painkiller UI and counter value
    public void PainKillerText(int value)
    {
        PainKillersCollected += value;
        PainKillerCounter.text = $"x{PainKillersCollected}";
    }

    //updates the gold UI and counter value
    public void GoldText()
    {
        coinCollectAudio.Play();
        GoldCollected++;
        GoldCounter.text = $"x{GoldCollected}/10";
        CheckAllGoldCollected();
        //if all gold ingots are collected the Temple Door will be opened
        if (objectiveComplete == true)
        {
            doorScript.OpenTempleDoor();
        }
    }

    //Method called to check if all gold ingots are collected
    public void CheckAllGoldCollected()
    {
        if(GoldCollected == 10)
        {
            objectiveComplete = true;
        }
    }

    //returns the actual IInteractable object closest to player or null
    public IInteractable CheckNearestObject()
    {
        //getting the nearest game object to do action if keybind pressed
        GameObject nearestGameObject = GetClosestObject();
        if (nearestGameObject != null)
        {
            IInteractable interactable = nearestGameObject.GetComponent<IInteractable>();
            return interactable;
        }
        else 
        {
            return null;
        }
    }
}
