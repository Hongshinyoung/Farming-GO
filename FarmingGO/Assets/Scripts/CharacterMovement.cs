using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
 public Transform cameraTransform;
    public CharacterController controller;

    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float gravity = -9.81f;
    public float yVelocity = 0;

    PlayerInteraction playerInteraction;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>(); 

        if (Input.GetKey(KeyCode.RightBracket))
        {
            TimeManager.Instance.Tick();
        }
    }

    void Update()
    {
        Move();
        Interact();
    }

    public void Move() 
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(h, 0, v);

        moveDirection = cameraTransform.TransformDirection(moveDirection);

        moveDirection *= moveSpeed;
        if (moveDirection != Vector3.zero)
        {
            gameObject.transform.forward = moveDirection;
        }

        if (controller.isGrounded)
        {
            yVelocity = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                yVelocity = jumpSpeed;
            }
        }

        yVelocity += (gravity * Time.deltaTime);
        moveDirection.y = yVelocity;
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void Interact() 
    {
        //Tool interaction
        if (Input.GetButtonDown("Fire1"))
        {
            //Interact
            playerInteraction.Interact();
        }

    }
}
