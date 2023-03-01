using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonActions : MonoBehaviour
{
    GameObject gameManager;
    gameManager gameManagerScript;
    pauseGame gameManagerPauseScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager");
        gameManagerScript = gameManager.GetComponent<gameManager>();
        gameManagerPauseScript = gameManager.GetComponent<pauseGame>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuStart");
    }

    public void GameOver()
    {
        gameManagerScript.canMove = true;
        gameManagerPauseScript.ResumeGame();
        gameManagerPauseScript.HideUIPause();
        SceneManager.LoadScene("MenuOver");
    }

    public void Leave()
    {
        Application.Quit();
    }

    public void Continue()
    {
        gameManagerScript.canMove = true;
        gameManagerPauseScript.ResumeGame();
        gameManagerPauseScript.HideUIPause();
    }
}
