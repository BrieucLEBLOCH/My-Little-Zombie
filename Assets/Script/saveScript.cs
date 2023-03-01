using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveScript : MonoBehaviour
{
    [SerializeField] Vector3 checkPointPosition;
    // Start is called before the first frame update
    void Awake()
    {
        checkPointPosition = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePosition(Vector3 pos)
    {
        checkPointPosition = pos;
    }

    public Vector3 LoadPosition()
    {
        return checkPointPosition;
    }
}
