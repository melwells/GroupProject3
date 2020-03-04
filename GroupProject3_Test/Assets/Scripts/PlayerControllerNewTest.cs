using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNewTest : MonoBehaviour
{
    private Vector3 moveVector;
    private Vector3 lastMove;
    public float speed;
    public float jumpforce;

    private float gravity = 25;
    private float verticalVelocity;
    private CharacterController controller;

    public Transform groundCheck;
    public Transform attackPoint;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>(); 
    }

    void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Checks to see if object is on the ground
    }

    void Update()
    {
        //Movement and Jumping
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal"); //X axis input
        moveVector.z = Input.GetAxis("Vertical"); //Z axis input

        moveVector = transform.right * moveVector.x + transform.forward * moveVector.z; //Adjusts inputs to move character relative to the camera

        if (controller.isGrounded)
        {
            verticalVelocity = -1; //Keeps character on the ground unless an input is provided

            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpforce; //Causes the character to jump
            }
        }
        else //Slowly brings object back to the ground, and locks in direction of the jump
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }

        moveVector.y = 0; //Resets y vector in case of lastMove
        moveVector.Normalize();
        moveVector *= speed; //Adjusts how fast the character moves
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;

        //Combat
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    //Wall Jumping
    void OnControllerColliderHit(ControllerColliderHit hit) //Detects that the character is colliding with an object
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f) //Checks that the character isn't grounded and also colliding with object that has a steep slope
        {
            if(Input.GetButtonDown("Jump")) //Bounces character away from the wall
            {
                verticalVelocity = jumpforce;
                moveVector = hit.normal * speed;
            }
        }
    }

    void Attack()
    {

    }
}
