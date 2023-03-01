using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementCamera : MonoBehaviour
{
    public float _verticalSpeed = 500.0f;

    GameObject gameManager;
    gameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        new Vector3(15.0f, -0.272f, 0.967f);

        gameManager = GameObject.Find("Manager");
        gameManagerScript = gameManager.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalOffset = _verticalSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;
        if (transform.eulerAngles.x - verticalOffset < 45.0f && transform.eulerAngles.x - verticalOffset > 0.0f && gameManagerScript.canMove)
        {
            transform.Rotate(-verticalOffset, 0.0f, 0.0f, Space.Self);
        }
    }
}
