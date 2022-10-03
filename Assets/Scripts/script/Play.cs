using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(EnterBattleScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SendPlayRequest() {

        InstanceManager.instance.netClient.Send(new CSEnterRoomMsg());

    }


    public void EnterBattleScene() {


        InstanceManager.instance.netClient.Send(new CSEnterRoomMsg());


        
    }

}


