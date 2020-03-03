using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
  //invisibility variables
  public MeshRenderer playerMesh; //pull mesh from player model
  public Material opaqueMat;
  public Material transparentMat;

  private float cooldownTimer = 5.0f; //timer for cooldown
  private float invisibilityTimer = 5.0f; //timer for invisibility duration
  private float timer = 0.0f; //base Time control timer

  private bool cooldownActive = false; //is the cooldown active

    void Update()
    {

      //if the player presses F
      if (Input.GetKeyDown(KeyCode.F))
      {
        //if cooldown is not on, run invisibility and set cooldown on
        if (cooldownActive == false)
        {
          Invisible();
          cooldownActive = true;
        }

        if (Time.time > cooldownTimer)
        {
          timer = Time.time + cooldownTimer;
          cooldownActive = false;
        }

      /*  cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
          cooldownActive = false;
        } */

        //if cooldown is on, wait for timer to run its course and then turn cooldown off
      /*  else (cooldownActive != false)
        {
          cooldownTimer -= Time.deltaTime;
          if (cooldownTimer <= 0)
          {
            cooldownActive = false;
          }
        } */
      }

      //function to use Invisibility
      void Invisible()
      {
        //reduce alpha, go invisible
        playerMesh.GetComponent<MeshRenderer>().material = transparentMat;

        //return to normal after a set time
        /* invisibilityTimer -= Time.deltaTime;
        if (invisibilityTimer <= 0)
        {
          playerMesh.GetComponent<MeshRenderer>().material = opaqueMat;
        } */

        /* if (Time.time > timer)
        {
          timer = Time.time + invisibilityTimer;
          playerMesh.GetComponent<MeshRenderer>().material = opaqueMat;
        } */
      }
    }
}
