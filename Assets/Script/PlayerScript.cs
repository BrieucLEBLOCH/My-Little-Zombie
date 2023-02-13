using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f, rot = 70f, _jumpForce = 1.0f;
    [SerializeField] private AudioClip playerClip;
    AudioSource playerAudio;
    Animator teacherAnim;
    Animator doorOpen;
    [SerializeField] Vector3 _jump;
    [SerializeField] bool _isGrounded;
    [SerializeField] Rigidbody _rb;

    private void Awake()
    {
        teacherAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody>();
        _jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        transform.Translate(Vector3.right * speed * h * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * v * Time.deltaTime);

        if (h != 0)
        {
            if (h > 0) { teacherAnim.SetInteger("walking", 3); }
            if (h < 0) { teacherAnim.SetInteger("walking", 4); }
        }
        else if (h == 0)
        {
            if(v > 0) { teacherAnim.SetInteger("walking", 1); }
            else if (v < 0) { teacherAnim.SetInteger("walking", 2); }
                //playerAudio.PlayOneShot(playerClip);
        }
        if (h ==0 && v == 0) { teacherAnim.SetInteger("walking", 0); }

        // dans le on update
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            teacherAnim.SetBool("jump", true);
            _isGrounded = false;
            _rb.AddForce(_jump * _jumpForce, ForceMode.Impulse);
        }


        

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Door")
        {
            doorOpen = collision.gameObject.GetComponentInParent<Animator>();
            doorOpen.SetBool("isOpen", true);
        }
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        for (int i = 0; i < collisionInfo.contactCount; i++)
        {
            Debug.Log("test");
            if (transform.position.x == collisionInfo.GetContact(i).point.x)
            {
                //_isGrounded = true;
            }

            if (collisionInfo.gameObject.tag == "Ennemy")
            {
                // player.Hurt();
            }
        }
    }

    void JumpEnd()
    { _isGrounded = true; }

}
