using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public VariableJoystick joy;
    public GameOverManager overManager;
    //Movement Components
    private CharacterController controller;
    private Animator animator;
    MeshRenderer[] meshs;

    [Header("Movement System")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;

    public int health;

    private bool isRunning = false;

    bool isDamage;
    bool isDead;


    //Interaction components
    PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        //Get movement components
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        //Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteraction>();

        meshs = GetComponentsInChildren<MeshRenderer>();

    }
    public void ToggleRunning()
    {
        isRunning = !isRunning;
    }

    // Update is called once per frame
    void Update()
    {
        //Runs the function that handles all movement
        Move();

        //Runs the function that handles all interaction
        //Interact();

        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    //public void Interact()
    //{
    //    //Tool interaction
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        //Interact
    //        playerInteraction.Interact();
    //    }

    //    //Item interaction
    //    if (Input.GetButtonDown("Fire2"))
    //    {
    //        playerInteraction.ItemInteract();
    //    }

    //    //TODO: Set up item interaction
    //}

    public void Move()
    {
        //Get the horizontal and vertical inputs as a number
        float horizontal = joy.Horizontal;
        float vertical = joy.Vertical;

        float speed = isRunning ? runSpeed : walkSpeed;

        //Direction in a normalised vector
        Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = speed * Time.deltaTime * dir;

        if (speed == runSpeed)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        //Is the sprint key pressed down?
        //if (Input.GetButton("Sprint"))
        //{
        //    //Set the animation to run and increase our movespeed
        //    moveSpeed = runSpeed;
        //    animator.SetBool("Running", true);
        //}
        //else
        //{
        //    //Set the animation to walk and decrease our movespeed
        //    moveSpeed = walkSpeed;
        //    animator.SetBool("Running", false);
        //}


        //Check if there is movement
        if (dir.magnitude >= 0.1f)
        {
            //Look towards that direction
            transform.rotation = Quaternion.LookRotation(dir);

            //Move
            controller.Move(velocity);

        }

        //Animation speed parameter
        animator.SetFloat("Speed", velocity.magnitude);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyAttack")
        {
            if (!isDamage)
            {
                EnemyAttack enemyAttack = other.GetComponent<EnemyAttack>();
                health -= enemyAttack.damage;
                StartCoroutine(OnDamage());
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        } 
        yield return new WaitForSeconds(1f);
        isDamage = false;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }
        
        if(health <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        isDead = true;
        overManager.GameOver();
    }
}