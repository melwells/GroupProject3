using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

  public float mouseSensitivity = 100f;
  public Transform playerBody;

  float xRotation = 0f;


    // Start is called before the first frame update
    void Start()
    {
      Cursor.lockState = CursorLockMode.Locked; //prevent cursor from leaving screen
    }

    // Update is called once per frame
    void Update()
    {
      //rotate camera depending on mouse movement. horizontal and vertical rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation = xRotation - mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //prevent looking all the way around

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
