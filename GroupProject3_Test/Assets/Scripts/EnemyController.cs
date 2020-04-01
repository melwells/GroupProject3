using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public CooldownTest player; //calling cooldowntest script for the invisBool variable

    public GameObject playerTarget;
    public float lookRadius = 10f;
    public NavMeshAgent agent;
    public Vector3[] patrolPoints;

    Transform target;

    int currentHealth;

    private int point;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        target = playerTarget.transform; //target the player
        agent = GetComponent<NavMeshAgent>();
        point = 0;
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
        //do not attack
        Patrol();
      }

      if (player.invisBool == false) //if the player is visible
      {
        //attack
        if (distance <= lookRadius)
        {
          agent.SetDestination(target.position);
          transform.LookAt(target.transform);
        }
        if (distance > lookRadius)
        {
          Patrol();
        }
      }
    }

    void Patrol()
    {
      gameObject.GetComponent<NavMeshAgent>().isStopped = false; //if not moving, move
      if (patrolPoints.Length > 0)
      {
        agent.SetDestination(patrolPoints[point]);
        if (transform.position == patrolPoints[point] || Vector3.Distance(transform.position, patrolPoints[point]) < 0.2f)
        {
          point++;
        }
        if (point >= patrolPoints.Length) //go back to start of patrol points
        {
          point = 0;
        }
      }
    }

    void OnDrawGizmosSelected() //debug purposes. See enemy's line of sight
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
