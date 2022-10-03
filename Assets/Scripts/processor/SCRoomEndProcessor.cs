using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using msg;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SCRoomEndProcessor : JsonProcessor<SCRoomEndMsg>
{

    


    public SCRoomEndProcessor() {
       
    }

    

    public override void process(SCRoomEndMsg hitMsg)
    {
        SceneUtil.LoadLobbyScene();

        InstanceManager.instance.playerManager.RemoveAllPlayerInfo();


    }
    
   
}
