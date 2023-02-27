using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class gameManager : MonoBehaviour
{
    [SerializeField] public int _childSave = 0;
    private int _childSaveMax = 0;
    [SerializeField] Text _textChildSave;

    GameObject[] listChild;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        listChild = GameObject.FindGameObjectsWithTag("Child");
        _childSaveMax = listChild.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F5))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        UpdateChildUI();

        if (_childSave == _childSaveMax)
        {
            SceneManager.LoadScene("MenuWin");
        }
    }

    private void UpdateChildUI()
    {
        // Update Text Quest
        _textChildSave.text = "Enfants : " + _childSave.ToString() + " / " + _childSaveMax.ToString();
    }
}
