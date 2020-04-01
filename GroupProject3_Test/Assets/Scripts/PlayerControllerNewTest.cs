using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerNewTest : MonoBehaviour
{
    private Vector3 moveVector;
    private Vector3 lastMove;
    public float speed;
    public float jumpforce;

    private float gravity = 25;
    private float verticalVelocity;
    private CharacterController controller;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyMask;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public int attackDamage = 50;
    public int lives;

    private int score;

    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPos = this.transform.position;
        lives = 5;
        score = 0;
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

   //Attacking, duh
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask); //Detecs enemies in attack range and stores in an array

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyController>().GetHit(attackDamage); //Damages each enemy within array/attack range
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.tag == "Enemy")
      {
        //reset position, lose lives
        Respawn();
        Debug.Log("lives =" + lives);
      }

      if (other.tag == "Coin")
      {
        score = score + 1;
        Debug.Log("score = " + score);
        Destroy(other.GetComponent<Collider>().gameObject);
      }

      if (other.tag == "EnergyDrink")
      {
        score = score + 10;
        Debug.Log("score = " + score);
        lives = 5;
        Debug.Log("lives = " + lives);
        Destroy(other.GetComponent<Collider>().gameObject);
      }
    }

    void WinGame()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      //SceneManager.LoadScene("Win");
    }

    void Respawn()
    {
      this.transform.position = originalPos;
      lives = lives - 1;

      if (lives <= 0)
      {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        //SceneManager.LoadScene("Lose");
      }
    }
}
