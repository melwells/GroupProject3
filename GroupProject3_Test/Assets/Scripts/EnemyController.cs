using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
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
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        this.enabled = false;
    }
}
