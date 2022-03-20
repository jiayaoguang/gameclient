using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject obj = GameObject.Find("Cube/ChildCube");
        if (obj == null)
        {
            Debug.Log("obj null //////");
        }
        else {
            Debug.Log("obj not null //////");
        }

        gameObject.name = "100000";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
