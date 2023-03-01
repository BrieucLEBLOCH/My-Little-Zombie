using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameCursorOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setVisible(bool visible)
    {
        Cursor.visible = visible;
    }
}
