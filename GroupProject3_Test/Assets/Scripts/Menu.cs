using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource click;

  public void StartGame()
  {
    //SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    SceneManager.LoadScene("Game");
        click.Play();

  }

    public void QuitGame()
    {
        Application.Quit();
        click.Play();
        Debug.Log ("Quit");
    }

    public void MainMenu()
    {
      SceneManager.LoadScene("Main");
    }
}
