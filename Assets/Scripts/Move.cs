using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public const float speed = 0.1f;

    public const int side_length = 500;

    // Start is called before the first frame update
    void Start()
    {
        
        transform.position = new Vector3(transform.position.x - speed, 0, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W)) {
            if (transform.position.y < side_length) { 
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, 0);
            }
            
        }
        else if(Input.GetKey(KeyCode.S)) {
            if (transform.position.y > -side_length)
            { 
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, 0);
            }
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x > -side_length)
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, 0);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x < side_length)
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y , 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            shoot();
        }


    }


    public void shoot() { 
    
    }

}
