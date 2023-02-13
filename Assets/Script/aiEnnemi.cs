using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class aiEnnemi : MonoBehaviour
{


    [SerializeField] private enum Phase
    {
        SIT,
        STANDING,
        CHASE,
    }
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Vector3 _startPosition = Vector3.zero;

    [SerializeField] private Phase _phase = Phase.CHASE;
    [SerializeField] private Vector3 playerPos;

    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get player positionzz
        playerPos = new Vector3(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y, GameObject.Find("Player").transform.position.z);

        // Distance beetween Player and ennemi
        double distWithPlayer = System.Math.Sqrt(
            System.Math.Pow((playerPos[0] - transform.position.x), 2) +
            System.Math.Pow((playerPos[2] - transform.position.z), 2)
            );

        UpdatePhase(distWithPlayer);

        if (_phase == Phase.SIT)
        {
            Sit();
        }
        else if (_phase == Phase.STANDING)
        {
            Standing();
        }
        else if (_phase == Phase.CHASE)
        {
            Chase();
        }
    }

    private void Sit()
    {
        // Back to the starting point
        Back();

        // Or Sit
    }

    private void Standing()
    {
        // stand up
    }

    private void Chase()
    {
        // Target Player
        Vector3 playerTarget = playerPos;
        playerTarget.y = transform.position.y;
        transform.LookAt(playerTarget);

        // Move forward
        transform.Translate(Vector3.forward * Time.deltaTime * _speed / 10.0f);
    }

    private void Back()
    {
        // Target Spawn
        Vector3 startTarget = _startPosition;
        startTarget.y = transform.position.y;
        transform.LookAt(startTarget);

        // Move to the start point
        transform.Translate(Vector3.forward * Time.deltaTime * _speed / 10.0f);
    }

    void UpdatePhase(double distWithPlayer)
    {
        if (distWithPlayer <= 15.0f)
        {
            _phase = Phase.CHASE;
        }
        else if (distWithPlayer <= 20.0f)
        {
            _phase = Phase.STANDING;
        }
        else
        {
            _phase = Phase.SIT;
        }
    }
}
