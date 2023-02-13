using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChildScript : MonoBehaviour
{
    NavMeshAgent childAgent;
    [SerializeField] Transform targetSchool;
    [SerializeField] private bool bIsInChildSave = false;
    [SerializeField] private Collider childGameCollider;

    // Start is called before the first frame update
    void Start()
    {
        childAgent = GetComponent<NavMeshAgent>();
        childAgent.speed = 5;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (bIsInChildSave)
            {
                childAgent.SetDestination(targetSchool.position);
                //addSaveChild();
            }
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxPlayer") bIsInChildSave = true;
        if (collisionInfo.tag == "HitboxSchool")
        {
            //Destroy(gameObject);
            //Destroy(childGameCollider.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxPlayer") bIsInChildSave = false;
    }

    //public void addSaveChild()
    //{
    //    _childSave++;
    //}
}
