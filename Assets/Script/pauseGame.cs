using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    [SerializeField] GameObject menuPause;

    GameObject gameManager;
    gameCursorOff gameCursorScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Manager");
        gameCursorScript = gameManager.GetComponent<gameCursorOff>();

        HideUIPause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GamePause()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {

        Time.timeScale = 1;
    }

    public void ShowUIPause()
    {
        menuPause.SetActive(true);
        gameCursorScript.setVisible(true);
    }

    public void HideUIPause()
    {
        menuPause.SetActive(false);
        gameCursorScript.setVisible(false);
    }
}
