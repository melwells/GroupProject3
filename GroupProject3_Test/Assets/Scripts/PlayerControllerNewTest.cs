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
    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }
    private void Update()
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
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
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
}
