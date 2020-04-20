using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource click;
    public Text highScore;

    void Start()
    {
      highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

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

    public void HowToPlay()
    {
      SceneManager.LoadScene("HowToPlay");
    }

    public void MainMenu()
    {
      SceneManager.LoadScene("Main");
    }

    public void DeleteHS()
    {
      PlayerPrefs.DeleteKey("HighScore");
      highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }

}
