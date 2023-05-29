using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement Components
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    //private CharacterController controller;
    private Animator animator;

    //private float moveSpeed = 4f;

    //[Header("Movement System")]
    //public float walkSpeed = 4f;
    //public float runSpeed = 8f;


    //Interaction components
    PlayerInteraction playerInteraction;

    // Start is called before the first frame update
    void Start()
    {
        //Get movement components
        //controller = GetComponent<CharacterController>();
        animator = characterBody.GetComponent<Animator>();

        //Get interaction component
        playerInteraction = GetComponentInChildren<PlayerInteraction>();

    }

    // Update is called once per frame
    void Update()
    {
        //Runs the function that handles all movement
        //Move();

        //Runs the function that handles all interaction
        Interact();

        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    public void Interact()
    {
        //Tool interaction
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerInteraction.Interact();
        }

        //Item interaction
        if (Input.GetButtonDown("Fire2"))
        {
            playerInteraction.ItemInteract();
        }

        //TODO: Set up item interaction
    }

    public void Move(Vector2 inputDirection)
    {
        Vector2 moveInput = inputDirection;
        bool isMove = moveInput.magnitude != 0;
        animator.SetBool("isMove", isMove);
        if (isMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    //public void Move()
    //{
    //    //Get the horizontal and vertical inputs as a number
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");

    //    //Direction in a normalised vector
    //    Vector3 dir = new Vector3(horizontal, 0f, vertical).normalized;
    //    Vector3 velocity = moveSpeed * Time.deltaTime * dir;

    //    //Is the sprint key pressed down?
    //    if (Input.GetButton("Sprint"))
    //    {
    //        //Set the animation to run and increase our movespeed
    //        moveSpeed = runSpeed;
    //        animator.SetBool("Running", true);
    //    }
    //    else
    //    {
    //        //Set the animation to walk and decrease our movespeed
    //        moveSpeed = walkSpeed;
    //        animator.SetBool("Running", false);
    //    }


    //    //Check if there is movement
    //    if (dir.magnitude >= 0.1f)
    //    {
    //        //Look towards that direction
    //        transform.rotation = Quaternion.LookRotation(dir);

    //        //Move
    //        controller.Move(velocity);

    //    }

    //    //Animation speed parameter
    //    animator.SetFloat("Speed", velocity.magnitude);



    //}
}