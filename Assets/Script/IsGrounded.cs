using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool _isGrounded;
    // Start is called before the first frame update
    void Start()
    {

        _isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "IsGroundedHitbox") _isGrounded = true;
    }
}
