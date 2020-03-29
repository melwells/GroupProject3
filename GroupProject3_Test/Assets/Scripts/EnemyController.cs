using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    public CooldownTest player; //calling cooldowntest script for the invisBool variable

    int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
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
      if (player.invisBool == true) //if the player is invisible
      {
        //do not attack
      }

      if (player.invisBool == false) //if the player is visible
      {
        //attack
      }
    }
}
