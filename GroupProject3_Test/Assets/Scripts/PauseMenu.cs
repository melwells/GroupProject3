using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          if (isPaused)
          {
            Resume();
          }
          else
          {
            Pause();
          }
        }
    }

    public void Resume()
    {
      pauseMenuUI.SetActive(false);
      Time.timeScale = 1f;
      isPaused = false;
      Cursor.visible = false;
    }

    void Pause()
    {
      pauseMenuUI.SetActive(true);
      Time.timeScale = 0f;
      isPaused = true;
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None; //unlock mouse for menu
    }

    public void Menu()
    {
      Debug.Log("Main menu loading..");
      Time.timeScale = 1f;
      SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
      Debug.Log("Quit");
      Application.Quit();
    }
}
