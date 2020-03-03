using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTest : MonoBehaviour
{
  //invisibility variables
  public MeshRenderer playerMesh; //pull mesh from player model
  public Material opaqueMat;
  public Material transparentMat;

  public float cooldownTime; //control how long cooldown lasts
  public float invisibleTime; //how long you can stay invis for

  private float nextInvisibilityUse; //when can you next use the ability


  //private bool cooldownActive = false; //is the cooldown active

    void Update()
    {

      if (Time.time > nextInvisibilityUse) //if you can use it
      {
        if (Input.GetKeyDown(KeyCode.F))
        {
          invisibleTime = 5f;
            Invisible();
            nextInvisibilityUse = Time.time + cooldownTime; //countdown to next ability use 
          }
        }
    }

/*    void Invisible()
    {
      //reduce alpha, go invisible
      playerMesh.GetComponent<MeshRenderer>().material = transparentMat;
    } */

      //function to use Invisibility

      void Invisible()
      {

        if (invisibleTime > 0f)
        {
          //reduce alpha, go invisible
          playerMesh.GetComponent<MeshRenderer>().material = transparentMat;
          invisibleTime -= Time.deltaTime;
        }

        else if (invisibleTime <= 0f)
        {
          playerMesh.GetComponent<MeshRenderer>().material = opaqueMat;
        }

      }
}
