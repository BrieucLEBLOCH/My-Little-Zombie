using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    [SerializeField] GameObject menuPause;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void HideUIPause()
    {
        menuPause.SetActive(false);
    }
}
