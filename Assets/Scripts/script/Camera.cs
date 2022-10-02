using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Camera : MonoBehaviour
{

    public GameObject playerGo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(playerGo.transform.position.x, playerGo.transform.position.y, -100);
    }
}

