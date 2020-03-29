using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

  public CharacterController controller; //calls on controller attached to Player
  public float speed = 12f;
  public float gravity = -9.81f;
  public float jumpHeight = 3f;

//checking for whether or not player is touching the ground
  public Transform groundCheck;
  public float groundDistance = 0.4f;
  public LayerMask groundMask;

  Vector3 originalPos;
  Vector3 velocity; //help with gravity
  bool isGrounded; //are we on the ground

  private Vector3 move;
  void Start()
  {
    originalPos = this.transform.position;
  }

    // Update is called once per frame
    void FixedUpdate()
    {
      //checking for if we are on ground.
      isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

      if (isGrounded && velocity.y <0)
      {
        velocity.y = -2f;
      }


      //controlling movement
      float x = Input.GetAxis("Horizontal"); //x axis
      float z = Input.GetAxis("Vertical"); //z axis

      move = transform.right * x + transform.forward * z;

      controller.Move(move * speed * Time.deltaTime);

      //jump
      if(Input.GetButtonDown("Jump") && isGrounded)
      {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
      }

      //fall with gravity
      velocity.y += gravity * Time.deltaTime;

      controller.Move(velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f)
        {
            if(Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                move = hit.normal * speed;
            }
        }
    }

}
