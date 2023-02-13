using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class scriptPlayer : MonoBehaviour
{
    // movement 
    public float _speed = 5.0f;

    // camera
    public float _horizontalSpeed = 1000.0f;
    
    // jump
    public Vector3 _jump;
    public float _jumpForce = 200.0f;
    public bool _isGrounded;
    Rigidbody _rb;

    [SerializeField] private float _health = 10;
    [SerializeField] private float _healthMax = 10;
    [SerializeField] Component _healthComponent;

    [SerializeField] private float _stamina = 100;
    [SerializeField] private float _staminaMax = 100;
    [SerializeField] Component _staminaComponent;

    [SerializeField] private float _food = 10;
    [SerializeField] private float _foodMax = 10;
    [SerializeField] Component _foodComponent;

    [SerializeField] private int _childSave = 0;
    private int _childSaveMax = 0;
    [SerializeField] Text _textChildSave;

    [SerializeField] private float _timeFoodDeleteAll = 10.0f;

    private float _lastTimeStamina;
    [SerializeField] private float _timeStaminaGain = 1.0f;
    [SerializeField] private float _timeStaminaMaxUse = 5.0f;

    // Animations
    Animator teacherAnim;

    [SerializeField] private bool bIsInDistributeur = false;
    [SerializeField] private bool bIsInHospital = false;
    [SerializeField] private bool bIsInChildSave = false;

    [SerializeField] private Collider childGameCollider;

    GameObject[] listChild;

    // Start is called before the first frame update
    void Start()
    {
        teacherAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        _isGrounded = true;

        listChild = GameObject.FindGameObjectsWithTag("Child");
        _childSaveMax = listChild.Length;
    }

    void Update()
    {
        _speed = 5.0f;

        // Run
        if (Input.GetKey(KeyCode.LeftShift) && _stamina > 0.0f)
        {
            float staminaRemove = Time.deltaTime * 100 / _timeStaminaMaxUse;
            addStamina(-staminaRemove);
            _speed *= 2.0f;
            _lastTimeStamina = Time.time;
        }

        // Gain stamina when you don't run 
        if (_lastTimeStamina + _timeStaminaGain < Time.time && _stamina < _staminaMax) addStamina(Time.deltaTime * 100 / 3.0f);

        // Lose food bar, in _timeFoodDeleteAll secondes, the food goes from 100 to 0.
        if (_food > 0.0f) addFood(-Time.deltaTime * 100 / _timeFoodDeleteAll);
        else SceneManager.LoadScene("MenuOver");

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h != 0 && v != 0)
        {
            _speed /= 1.5f;
        }
        transform.Translate(Vector3.right * _speed * h * Time.deltaTime);
        transform.Translate(Vector3.forward * _speed * v * Time.deltaTime);

        // Saut
        if (Input.GetKeyDown(KeyCode.Space))
        {
            teacherAnim.SetTrigger("jump");

            _rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
        }

        // Animations
        if (h != 0)
        {
            if (h > 0) { teacherAnim.SetInteger("walking", 3); }
            if (h < 0) { teacherAnim.SetInteger("walking", 4); }
        }
        else if (h == 0)
        {
            if (v > 0) { teacherAnim.SetInteger("walking", 1); }
            else if (v < 0) { teacherAnim.SetInteger("walking", 2); }
            //playerAudio.PlayOneShot(playerClip);
        }
        if (h == 0 && v == 0) { teacherAnim.SetInteger("walking", 0); }

        // Gagner de la nourriture
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bIsInDistributeur) addFood(15);
            if (bIsInHospital) addHealth(_healthMax);
            if (bIsInChildSave && childGameCollider != null)
            {
                Destroy(childGameCollider.gameObject);
                Destroy(childGameCollider.transform.parent.gameObject);

                addSaveChild();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateUI();

        // Contrôles souris
        float horizontalOffset = _horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(0.0f, horizontalOffset, 0.0f, Space.World);

        if (_childSave == _childSaveMax)
        {
            SceneManager.LoadScene("MenuWin");
        }

        if (_health <= 0)
        {
            SceneManager.LoadScene("MenuOver");
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        for (int i = 0; i < collisionInfo.contactCount; i++)
        {
            if (transform.position.x == collisionInfo.GetContact(i).point.x)
            {
                _isGrounded = true;
            }

            if (collisionInfo.gameObject.tag == "Ennemy")
            {
                addHealth(-1);
            }
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxEnnemyKill")
        {
            Destroy(collisionInfo.transform.parent.gameObject);
            _rb.AddForce(Vector3.up * _jumpForce * Time.deltaTime, ForceMode.Impulse);
        }
        else if (collisionInfo.tag == "HitboxDistributerBuy") bIsInDistributeur = true;
        else if (collisionInfo.tag == "HitboxHospitalHeal") bIsInHospital = true;
        else if (collisionInfo.tag == "HitboxChildSave") bIsInChildSave = true;
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxDistributerBuy") bIsInDistributeur = false;
        else if (collisionInfo.tag == "HitboxHospitalHeal") bIsInHospital = false;
        else if (collisionInfo.tag == "HitboxChildSave") bIsInChildSave = false;
    }

    private void OnTriggerStay(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxChildSave")
        {
            childGameCollider = collisionInfo;

            // Afficher : "Appuyer sur E Sauver"
            //Print()
        }
        else
        {
            childGameCollider = null;
        }
        if (collisionInfo.tag == "HitboxDistributerBuy")
        {
            // Afficher : "Appuyer sur E Acheter"
            //Print()
        }
        if (collisionInfo.tag == "HitboxHospitalHeal")
        {
            // Afficher : "Appuyer sur E Se Soigner"
            //Print()
        }
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

    public void addSaveChild()
    {
        _childSave++;
    }

    public void addFood(float food)
    {
        if (_food + food > _foodMax) _food = _foodMax;
        else _food += food;
    }

    public void addStamina(float stamina)
    {
        if (_stamina + stamina > _staminaMax) _stamina = _staminaMax;
        else _stamina += stamina;
    }

    public void addHealth(float health)
    {
        if (_health + health > _healthMax) _health = _healthMax;
        else _health += health;
    }

    private void UpdateUI()
    {
        // Update Text Quest
        _textChildSave.text = "Enfants : " + _childSave.ToString() + " / " + _childSaveMax.ToString();

        UpdateHealth();
        UpdateStamina();
        UpdateFood();
    }
}
