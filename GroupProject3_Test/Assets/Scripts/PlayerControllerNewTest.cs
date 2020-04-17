using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerNewTest : MonoBehaviour
{
    public GameObject player;
    public Transform Level2SpawnPoint;
    public Transform Level3SpawnPoint;

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

    public AudioSource[] playerSounds;
    public AudioSource a_jump;
    public AudioSource a_run;
    public AudioSource a_attack;

    public AudioSource a_coin;
    public AudioSource a_powerup;

    Vector3 originalPos;

    Animator anim;

    bool keyCard1;
    bool keyCard2;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalPos = this.transform.position;
        lives = 5;
        score = 0;
        anim = GetComponent<Animator>();

        playerSounds = GetComponents<AudioSource>();
        a_jump = playerSounds[0];
        a_run = playerSounds[1];
        a_attack = playerSounds[2];
        a_coin = playerSounds[3];
        a_powerup = playerSounds[4];

        keyCard1 = false;
        keyCard2 = false;
    }

    void FixedUpdate()
    {
        bool isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); //Checks to see if object is on the ground
    }

    void Update()
    {
        anim.SetInteger("lizCondition", 2);

        //Movement and Jumping
        moveVector = Vector3.zero;
        moveVector.x = Input.GetAxis("Horizontal"); //X axis input
        moveVector.z = Input.GetAxis("Vertical"); //Z axis input

        moveVector = transform.right * moveVector.x + transform.forward * moveVector.z;
        //Adjusts inputs to move character relative to the camera

        if (controller.isGrounded)
        {
            verticalVelocity = -1; //Keeps character on the ground unless an input is provided

            if(Input.GetButtonDown("Jump"))
            {
              verticalVelocity = jumpforce; //Causes the character to jump
                a_jump.Play();
              anim.SetInteger("lizCondition", 1);
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



        if (!Input.anyKey)
        {
          {
            anim.SetInteger("lizCondition", 0);
                a_run.Stop();
          }
        }
        else
        {
            a_run.Play();
        }

        //Combat
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("lizCondition", 3);
            Attack();
            a_attack.Play();
        }

        if (keyCard1 == true)
        {
          Level2();
          keyCard1 = false;
        }

        if (keyCard2 == true)
        { Level3();
          keyCard2 = false;
        }
    }

    //Wall Jumping
    void OnControllerColliderHit(ControllerColliderHit hit) //Detects that the character is colliding with an object
    {
        if (!controller.isGrounded && hit.normal.y < 0.1f) //Checks that the character isn't grounded and also colliding with object that has a steep slope
        {
            if(Input.GetButtonDown("Jump")) //Bounces character away from the wall
            {
                anim.SetInteger("lizCondition", 1);
                verticalVelocity = jumpforce;
                moveVector = hit.normal * speed;
                a_jump.Play();
            }
        }
    }

   //Attacking, duh
    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyMask); //Detecs enemies in attack range and stores in an array

        foreach(Collider enemy in hitEnemies)
        {
            enemy.GetComponent<DoorEnemyController>().GetHit(attackDamage); //Damages each enemy within array/attack range
        }
    }

    void OnTriggerEnter(Collider other)
    {
      if (other.tag == "DoorEnemy")
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
            a_coin.Play();
      }

      if (other.tag == "EnergyDrink")
      {
        score = score + 10;
        Debug.Log("score = " + score);
        lives = 5;
        Debug.Log("lives = " + lives);
        Destroy(other.GetComponent<Collider>().gameObject);
            a_powerup.Play();
      }

      if (other.tag == "KeyCard")
      {
        Debug.Log("picked up");
        Destroy (other.GetComponent<Collider>().gameObject);
        //Level2();
        keyCard1 = true;
      }

     if(other.tag == "KeyCard2")
      {
        Debug.Log("picked up");
        Destroy (other.GetComponent<Collider>().gameObject);
        //Level2();
        keyCard2 = true;
      }

      if (other.tag == "FinalKeyCard")
      {
        WinGame();
            a_coin.Play();
      }
    }

    void Level2()
    {
      player.transform.position = Level2SpawnPoint.position;
      Debug.Log(player.transform.position * Time.deltaTime);
    }

    void Level3()
    {
      player.transform.position = Level3SpawnPoint.position;
      Debug.Log(player.transform.position * Time.deltaTime);
    }

    void WinGame()
    {
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
      SceneManager.LoadScene("Win");
    }

    void Respawn()
    {
      this.transform.position = originalPos;
      lives = lives - 1;

      if (lives <= 0)
      {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Lose");
      }
    }
}
