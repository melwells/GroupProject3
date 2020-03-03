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
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    //invisibility variables
    public MeshRenderer playerMesh; //pull mesh from player model
    float cooldownTimer = 1;
    float invisibilityTimer = 1;

    private bool cooldownActive = false; //is the cooldown active

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    void Update()
    {
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.z = Input.GetAxis("Vertical"); //z axis

        moveVector = transform.right * moveVector.x + transform.forward * moveVector.z;

        if (controller.isGrounded)
        {
            verticalVelocity = -1;
            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpforce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            moveVector = lastMove;
        }

        moveVector.y = 0;
        moveVector.Normalize();
        moveVector *= speed;
        moveVector.y = verticalVelocity;

        controller.Move(moveVector * Time.deltaTime);
        lastMove = moveVector;

        //if the player presses F
        if (Input.GetKeyDown(KeyCode.F))
        {
          //if cooldown is not on, run invisibility and set cooldown on
          if (cooldownActive == false)
          {
            Invisible();
            cooldownActive = true;
          }

          //if cooldown is on, wait for timer to run its course and then turn cooldown off
          else if (cooldownActive != false)
          {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0)
            {
              cooldownActive = false;
            }
          }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if(Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpforce;
                moveVector = hit.normal * speed;
            }
        }
        Debug.DrawRay(hit.point, hit.normal, Color.red, 1.25f);
    }

    //function to use Invisibility
    void Invisible()
    {
      //reduce alpha, go invisible
      playerMesh.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
      //return to normal after a set time
      invisibilityTimer -= Time.deltaTime;
      if (invisibilityTimer <= 0)
      {
        playerMesh.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
      }
    }

}
