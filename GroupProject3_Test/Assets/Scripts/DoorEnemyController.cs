using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorEnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public CooldownTest player; //calling cooldowntest script for the invisBool variable

    public GameObject playerTarget;
    public float lookRadius = 5f;
    public NavMeshAgent agent;

    Vector3 startPos;

    Transform target;

    int currentHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = playerTarget.transform; //target the player
        agent = GetComponent<NavMeshAgent>();
        startPos = this.transform.position;
    }

    void FixedUpdate()
    {
      Attack();
    }

    public void GetHit(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        //Disables enemy without destroying it
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        this.enabled = false;
    }

    void Attack()
    {
      float distance = Vector3.Distance(target.position, transform.position);

      if (player.invisBool == true) //if the player is invisible
      {
        //this.transform.position = startPos;
        agent.SetDestination(startPos); //walk back to door
      }

      if (player.invisBool == false) //if the player is visible
      {
        //attack, chase player
        if (distance <= lookRadius)
        {
          agent.SetDestination(target.position);
          transform.LookAt(target.transform);
        }
      }
    }


    void OnDrawGizmosSelected() //debug purposes. See enemy's line of sight
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
