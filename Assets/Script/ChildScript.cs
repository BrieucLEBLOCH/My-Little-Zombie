using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.AI;


public class ChildScript : MonoBehaviour
{
    GameObject gameManager;
    gameManager gameManagerScript;

    NavMeshAgent childAgent;
    [SerializeField] Transform targetSchool;
    [SerializeField] private bool bIsInChildSave = false;
    Transform playerTransform;

    saveScript scriptPlayerSave;

    // Start is called before the first frame update
    void Start()
    {
        gameManager =  GameObject.Find("Manager");
        gameManagerScript = gameManager.GetComponent<gameManager>();
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
                scriptPlayerSave = playerTransform.GetComponent<saveScript>();

                if (scriptPlayerSave != null)
                {
                    scriptPlayerSave.SavePosition();
                    Debug.Log("CheckPoint set !");
                    Debug.Log(scriptPlayerSave.LoadPosition());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxPlayer")
        {
            bIsInChildSave = true;
            playerTransform = collisionInfo.transform;
        }
        
        if (collisionInfo.tag == "SchoolHitbox")
        {
            Destroy(gameObject);
            addSaveChild();
        }
    }

    private void OnTriggerExit(Collider collisionInfo)
    {
        if (collisionInfo.tag == "HitboxPlayer") bIsInChildSave = false;
    }

    public void addSaveChild()
    {
        gameManagerScript._childSave++;
    }
}
