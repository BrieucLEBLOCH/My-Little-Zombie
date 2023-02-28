using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    [SerializeField] Transform targetPlayer;
    [SerializeField] Transform targetChild;
    [SerializeField] float warningRadius;
    [SerializeField] float movingRadius;
    [SerializeField] float attackRadius;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        enemyAgent.SetDestination(targetPlayer.position);
        if (enemyAgent.remainingDistance <= movingRadius)
        {
            enemyAgent.speed = 5;
        }
        else
        {
            enemyAgent.speed = 0;
        }

    }
}
