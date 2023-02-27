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

    //stats
    GameObject gameManager;
    gameManager gameManagerScript;

    // Animations
    Animator teacherAnim;

    [SerializeField] private bool bIsInDistributeur = false;
    [SerializeField] private bool bIsInHospital = false;

    // Start is called before the first frame update
    void Start()
    {
        teacherAnim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();

        _isGrounded = true;

        gameManager = GameObject.Find("Manager");
        gameManagerScript = gameManager.GetComponent<gameManager>();
    }

    void Update()
    {
        _speed = 5.0f;

        // Run
        if (Input.GetKey(KeyCode.LeftShift) && gameManagerScript._stamina > 0.0f)
        {
            float staminaRemove = Time.deltaTime * 100 / gameManagerScript._timeStaminaMaxUse;
            addStamina(-staminaRemove);
            _speed *= 2.0f;
            gameManagerScript._lastTimeStamina = Time.time;
        }

        // Gain stamina when you don't run 
        if (gameManagerScript._lastTimeStamina + gameManagerScript._timeStaminaGain < Time.time && gameManagerScript._stamina < gameManagerScript._staminaMax) addStamina(Time.deltaTime * 100 / 3.0f);

        // Lose food bar, in _timeFoodDeleteAll secondes, the food goes from 100 to 0.
        if (gameManagerScript._food > 0.0f) addFood(-Time.deltaTime * 100 / gameManagerScript._timeFoodDeleteAll);
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
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _isGrounded = false;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bIsInDistributeur) addFood(15);
            if (bIsInHospital) addHealth(gameManagerScript._healthMax);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Contrôles souris
        float horizontalOffset = _horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        transform.Rotate(0.0f, horizontalOffset, 0.0f, Space.World);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        for (int i = 0; i < collisionInfo.contactCount; i++)
        {
            if (collisionInfo.gameObject.tag == "Ennemy")
            {
                addHealth(-1);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
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
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxDistributerBuy") bIsInDistributeur = false;
        else if (collisionInfo.tag == "HitboxHospitalHeal") bIsInHospital = false;
    }

    private void OnTriggerStay(Collider collisionInfo)
    {
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

    public void addFood(float food)
    {
        if (gameManagerScript._food + food > gameManagerScript._foodMax) gameManagerScript._food = gameManagerScript._foodMax;
        else gameManagerScript._food += food;
    }

    public void addStamina(float stamina)
    {
        if (gameManagerScript._stamina + stamina > gameManagerScript._staminaMax) gameManagerScript._stamina = gameManagerScript._staminaMax;
        else gameManagerScript._stamina += stamina;
    }

    public void addHealth(float health)
    {
        if (gameManagerScript._health + health > gameManagerScript._healthMax) gameManagerScript._health = gameManagerScript._healthMax;
        else gameManagerScript._health += health;
    }
}
