using msg;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ServerFrameProcessor : Processor
{

    private GameObject enemyPrefab;
    public ServerFrameProcessor() {

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
    }

    

    public void process(byte[] msg)
    {

        string str = System.Text.Encoding.Default.GetString(msg);

        ServerFrameMsg serverFrame = InstanceManager.instance.jsonManager.Deserialize<ServerFrameMsg>(str);


        Debug.Log(" receieve serverFrame ...... " + serverFrame.playerFrameMsgs.Count);

        foreach (PlayerFrameMsg playerFrameMsg in serverFrame.playerFrameMsgs) {

            if (playerFrameMsg.playerId == InstanceManager.instance.playerManager.myPlayerInfo.id) {
                continue;
            }

            PlayerInfo playerInfo = InstanceManager.instance.playerManager.GetPlayerInfo(playerFrameMsg.playerId);
            if (playerInfo == null)
            {
                GameObject gameObject = GameObject.Instantiate(enemyPrefab);

                
                gameObject.SetActive(true);

                playerInfo = new PlayerInfo();
                playerInfo.id = playerFrameMsg.playerId;
                playerInfo.gameObject = gameObject;
                InstanceManager.instance.playerManager.PutPlayerInfo(playerInfo);

                Debug.Log(" create enemy ");
            }

            playerInfo.gameObject.GetComponent<Transform>().position = new Vector3(playerFrameMsg.posi.x, playerFrameMsg.posi.y, 0);
        } 

        


    }

  
}
