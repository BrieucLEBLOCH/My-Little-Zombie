using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementPlayer : MonoBehaviour
{
    public float _speed = 5.0f;
    public float _horizontalSpeed = 1000.0f;
    public float _verticalSpeed = 1000.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.UpArrow)) transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) transform.Translate(Vector3.back * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow)) transform.Translate(Vector3.left * Time.deltaTime * _speed);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * Time.deltaTime * _speed);

        float horizontalOffset = _horizontalSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
        float verticalOffset = _verticalSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

        transform.Rotate(0.0f, horizontalOffset, 0.0f, Space.World);
    }
}
