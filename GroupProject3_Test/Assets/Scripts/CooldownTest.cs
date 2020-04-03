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

  public bool invisBool = false; //public bool for targets to see

  private float nextInvisibilityUse; //when can you next use the ability

  

  //private bool cooldownActive = false; //is the cooldown active

    void Update()
    {
        nextInvisibilityUse -= Time.deltaTime; //countdown for invis by frame
        if (nextInvisibilityUse <= 0f)
        {
            playerMesh.GetComponent<MeshRenderer>().material = opaqueMat;
            invisBool = false;
            Debug.Log("Invisible bool = " + invisBool); //debug notification. am I invisible?
            nextInvisibilityUse = cooldownTime;
        }

      {
        if (Input.GetKeyDown(KeyCode.F))
        {
            invisibleTime = 5f;
            Invisible();
            invisBool = true; //debug notification. am I invisible?
            Debug.Log("Invisible bool = " + invisBool);
            nextInvisibilityUse = Time.deltaTime + cooldownTime; //countdown to next ability use
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
          playerMesh.GetComponent<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0f); //Changes alpha of material to transparent

        }
      }
}
