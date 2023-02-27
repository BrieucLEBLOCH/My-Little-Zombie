using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class gameManager : MonoBehaviour
{
    [SerializeField] public float _health = 10;
    [SerializeField] public float _healthMax = 10;
    [SerializeField] public Component _healthComponent;

    [SerializeField] public float _stamina = 100;
    [SerializeField] public float _staminaMax = 100;
    [SerializeField] public Component _staminaComponent;

    [SerializeField] public float _food = 10;
    [SerializeField] public float _foodMax = 10;
    [SerializeField] public Component _foodComponent;

    [SerializeField] public float _timeFoodDeleteAll = 10.0f;

    public float _lastTimeStamina;
    [SerializeField] public float _timeStaminaGain = 1.0f;
    [SerializeField] public float _timeStaminaMaxUse = 5.0f;

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

        UpdateUI();

        if (_childSave == _childSaveMax)
        {
            SceneManager.LoadScene("MenuWin");
        }

        if (_health <= 0)
        {
            SceneManager.LoadScene("MenuOver");
        }
    }

    private void UpdateUI()
    {
        // Update Text Quest
        _textChildSave.text = "Enfants : " + _childSave.ToString() + " / " + _childSaveMax.ToString();
        UpdateHealth();
        UpdateStamina();
        UpdateFood();
    }

    private void UpdateHealth()
    {
        float pourcentHealth = _health * 100 / _healthMax;
        _healthComponent.transform.localScale = new Vector3(pourcentHealth * 2 / 100, 0.2f, 1f);
    }

    private void UpdateStamina()
    {
        float pourcentStamina = _stamina * 100 / _staminaMax;
        _staminaComponent.transform.localScale = new Vector3(pourcentStamina * 2 / 100, 0.2f, 1f);
    }

    private void UpdateFood()
    {
        float pourcentFood = _food * 100 / _foodMax;
        _foodComponent.transform.localScale = new Vector3(pourcentFood * 2 / 100, 0.2f, 1f);
    }
}
