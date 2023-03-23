using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
