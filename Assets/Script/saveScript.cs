using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveScript : MonoBehaviour
{
    Vector3 checkPointPosition;
    // Start is called before the first frame update
    void Start()
    {
        checkPointPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePosition()
    {
        checkPointPosition = transform.position;
    }

    public Vector3 LoadPosition()
    {
        return checkPointPosition;
    }
}
