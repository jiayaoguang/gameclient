using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public const float speed = 0.1f;

    public const int side_length = 500;

    public GameObject bulletPrefab;

    private LinkedList<GameObject> bulletQueue = new LinkedList<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x - speed, 0, transform.position.z);
        bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");
        for (int i = 0;i < 100; i++) {
            
            //GameObject bullet = Instantiate( bulletPrefab );
            //bullet.SetActive(true);
            //bullet.GetComponent<Transform>().position = new Vector3(10,0,10);
        }
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
