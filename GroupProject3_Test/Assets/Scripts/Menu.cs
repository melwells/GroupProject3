using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public void StartGame()
  {
    //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    SceneManager.LoadScene("Game");
  }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log ("Quit");
    }

    public void MainMenu()
    {
      SceneManager.LoadScene("Main");
    }
}
